using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // enemy properties
    public float speed = 1f;

    public float weaponCooldown = 1f;
    public float health = 1f;
    public float points = 10f;
    public MovType movType;

    public GameObject projectilePrefab;
    private float cooldownTimer = 0;
    private float movChangeTimer = 2f;

    private GameState gameState;
    private GameObject player;
    private float screenWidth;

    public enum MovType
    {
        Straight,
        Right,
        Left,
        TowardsPlayer
    }

    private void Start()
    {
        projectilePrefab.layer = 9;
        if (gameState == null)
            gameState = GameObject.FindObjectOfType<GameState>();

        screenWidth = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
        player = GameObject.FindGameObjectWithTag("Player");

        RandomizeMovType();
    }

    private void RandomizeMovType()
    {
        var rnd = new System.Random();
        movType = (MovType)Random.Range(0, 3);
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Shoot();

        if (health <= 0)
        {
            Destroy(gameObject);
            gameState.ScorePoints(points);
            gameState.kills++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        health--;
    }

    private void Shoot()
    {
        cooldownTimer -= Time.deltaTime;

        // shoot
        if (cooldownTimer <= 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.tag = "Projectile";
            cooldownTimer = weaponCooldown;
        }
    }

    private void Move()
    {
        movChangeTimer -= Time.deltaTime;

        // change randomly
        if (movChangeTimer <= 0)
        {
            RandomizeMovType();
            movChangeTimer = Random.Range(3, 6);
        }

        // invert movType at end of the screen
        if (transform.position.x > screenWidth && movType == MovType.Right)
            movType = MovType.Left;
        if (transform.position.x < -screenWidth && movType == MovType.Left)
            movType = MovType.Right;

        switch (movType)
        {
            case MovType.Right:
                GoDiagonalRight();
                break;

            case MovType.Left:
                GoDiagonalLeft();
                break;

            case MovType.TowardsPlayer:
                GoTowardsPlayer();
                break;

            case MovType.Straight:
            default:
                GoStraight();
                break;
        }
    }

    private void GoTowardsPlayer()
    {
        if (player.transform.position.x > transform.position.x)
            transform.Translate(new Vector3(-speed * Time.deltaTime, speed * Time.deltaTime, 0));
        if (player.transform.position.x < transform.position.x)
            transform.Translate(new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0));
    }

    private void GoDiagonalRight()
    {
        transform.Translate(new Vector3(-speed / 2 * Time.deltaTime, speed * Time.deltaTime, 0));
    }

    private void GoDiagonalLeft()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0));
    }

    private void GoStraight()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}