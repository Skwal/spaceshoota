using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // spaceship properties
    public float speed; // minimum 5, maximum 15?

    public float playerWidth; // 1
    public float weaponCooldown = 0.5f;
    private float cooldownTimer = 0;

    public GameObject projectilePrefab;
    private GameState gameState;

    private void Start()
    {
        projectilePrefab.layer = 8;
        if (gameState == null)
            gameState = GameObject.FindObjectOfType<GameState>();
    }

    private void Update()
    {
        MoveShip();
        Shoot();

        if (gameState.playerHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("GAME OVER! " + gameState.kills.ToString() + " kills!");
        }
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1"))
        {
            cooldownTimer -= Time.deltaTime;

            // shoot
            if (cooldownTimer <= 0)
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
                cooldownTimer = weaponCooldown;
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OUCH");
        gameState.playerHealth--;
    }
}