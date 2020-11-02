using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // spaceship properties
    public float speed; // minimum 5, maximum 15?

    public float playerWidth; // 1
    public float weaponCooldown = 0.5f;
    private float cooldownTimer = 0;
    public float numMissiles = 3f;
    public Vector3 projectileOffset = new Vector3(0, 0.4f, 0);

    public GameObject projectilePrefab, missilePrefab;
    private GameState gameState;
    private Health playerHealth;

    private enum AnimationState
    {
        Idle,
        Left,
        Right
    }

    AnimationState currentAnim = AnimationState.Idle;

    private void Start()
    {
        projectilePrefab.layer = 10;
        missilePrefab.layer = 8;
        if (gameState == null)
            gameState = GameObject.FindObjectOfType<GameState>();

        playerHealth = gameObject.GetComponent<Health>();
    }

    private void Update()
    {
        if (gameState.currentState == GameState.State.Playing)
        {
            MoveShip();
            Shoot();
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
            cooldownTimer = weaponCooldown;
        }

        // Shoot Missile
        if (Input.GetButton("Fire2") && cooldownTimer <= 0)
        {
            GameObject projectile = Instantiate(missilePrefab, transform.position + projectileOffset, transform.rotation);
            projectile.tag = "Projectile";
            cooldownTimer = weaponCooldown;
        }
    }

    private void MoveShip()
    {
        // Movement
        float screenWidth = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
        Vector3 pos = transform.position;
        float translation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // Animation
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (currentAnim != AnimationState.Left)
                GetComponentInChildren<Animator>().SetTrigger("Tilt");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            currentAnim = AnimationState.Left;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if (currentAnim != AnimationState.Right)
                GetComponentInChildren<Animator>().SetTrigger("Tilt");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            currentAnim = AnimationState.Right;
        }
        else
        {
            if (currentAnim != AnimationState.Idle)
                GetComponentInChildren<Animator>().SetTrigger("Idle");
            currentAnim = AnimationState.Idle;
        }

        // Screen boundaries
        if (pos.x + translation > screenWidth - playerWidth / 2)
            pos.x = screenWidth - playerWidth / 2;
        else if (pos.x + translation < -screenWidth + playerWidth / 2)
            pos.x = -screenWidth + playerWidth / 2;
        else
            pos.x += translation;

        transform.position = pos;
    }
}