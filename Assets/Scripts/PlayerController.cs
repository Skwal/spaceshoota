using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // spaceship properties
    public float speed; // minimum 5, maximum 15?

    public float playerWidth; // 1
    public float weaponCooldown = 0.5f;
    private float cooldownTimer = 0;
    public float numMissiles = 3f;

    public GameObject projectilePrefab, missilePrefab;
    private GameState gameState;
    private Health playerHealth;

    private void Start()
    {
        projectilePrefab.layer = 8;
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
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.tag = "Projectile";
            cooldownTimer = weaponCooldown;
        }

        // Shoot Missile
        if (Input.GetButton("Fire2") && cooldownTimer <= 0)
        {
            GameObject projectile = Instantiate(missilePrefab, transform.position, transform.rotation);
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