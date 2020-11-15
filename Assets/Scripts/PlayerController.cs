using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // spaceship properties
    public float speed; // minimum 5, maximum 15?

    public float playerWidth; // 1
    public float weaponCooldown = 0.5f;
    private float cooldownTimer = 0;
    public int numMissiles = 3;
    public int maxMissiles = 5;
    public Vector3 projectileOffset = new Vector3(0, 0.4f, 0);

    public GameObject projectilePrefab, missilePrefab;
    private GameState gameState;
    private Health playerHealth;
    private GameObject particleExplosion;

    private GameObject shields;

    private enum AnimationState
    {
        Idle,
        Left,
        Right
    }

    private AnimationState currentAnim = AnimationState.Idle;

    private void Start()
    {
        projectilePrefab.layer = 10;
        missilePrefab.layer = 8;
        if (gameState == null)
            gameState = GameObject.FindObjectOfType<GameState>();

        playerHealth = gameObject.GetComponent<Health>();
        particleExplosion = (GameObject)Resources.Load("Prefabs/ParticleExplosion", typeof(GameObject));

        shields = GameObject.FindGameObjectWithTag("Shields");
        shields.SetActive(true);
    }

    private void Update()
    {
        if (gameState.currentState == GameState.State.Playing)
        {
            MoveShip();
            Shoot();

            if (playerHealth.currentHealth <= 0)
            {
                GameObject explo = Instantiate(particleExplosion, transform.position, transform.rotation);
                Destroy(explo, 2f);
            }
        }
    }

    private void LateUpdate()
    {
        if (playerHealth.currentHealth <= 0)
        {
            gameObject.transform.position = new Vector3(0, -5.6f, 0);
            //Debug.Log("GAME OVER! " + gameState.kills.ToString() + " kills!");
        }
    }

    private void Shoot()
    {
        cooldownTimer -= Time.deltaTime;

        // Shoot laser
        if (Input.GetButton("Fire1") && cooldownTimer <= 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position + projectileOffset, transform.rotation);
            projectile.tag = "Projectile";
            projectile.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFX Volume");
            cooldownTimer = weaponCooldown;
        }

        // Shoot Missile
        if (Input.GetButton("Fire2") && cooldownTimer <= 0 && numMissiles > 0)
        {
            GameObject projectile = Instantiate(missilePrefab, transform.position + projectileOffset, transform.rotation);
            projectile.tag = "Projectile";
            projectile.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFX Volume");
            cooldownTimer = weaponCooldown;

            numMissiles--;
        }
    }

    private void MoveShip()
    {
        // Movement
        float screenWidth = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
        Vector3 pos = transform.position;
        float translationX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float translationY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // Animation
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (currentAnim != AnimationState.Left)
            {
                GetComponentInChildren<Animator>().SetTrigger("Tilt");
                transform.localScale = new Vector3(1, 1, 1);
                currentAnim = AnimationState.Left;
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if (currentAnim != AnimationState.Right)
            {
                GetComponentInChildren<Animator>().SetTrigger("Tilt");
                transform.localScale = new Vector3(-1, 1, 1);
                currentAnim = AnimationState.Right;
            }
        }
        else
        {
            if (currentAnim != AnimationState.Idle)
            {
                GetComponentInChildren<Animator>().SetTrigger("Idle");
                currentAnim = AnimationState.Idle;
            }
        }

        // Screen boundaries
        if (pos.x + translationX > screenWidth - playerWidth / 2)
            pos.x = screenWidth - playerWidth / 2;
        else if (pos.x + translationX < -screenWidth + playerWidth / 2)
            pos.x = -screenWidth + playerWidth / 2;
        else
            pos.x += translationX;

        if (pos.y + translationY > Camera.main.orthographicSize - playerWidth / 2)
            pos.y = Camera.main.orthographicSize - playerWidth / 2;
        else if (pos.y + translationY < -Camera.main.orthographicSize + playerWidth / 2)
            pos.y = -Camera.main.orthographicSize + playerWidth / 2;
        else
            pos.y += translationY;

        transform.position = pos;
    }
}