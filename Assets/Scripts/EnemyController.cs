using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // enemy properties
    public float speed = 1f;

    public int points = 10;
    public MovType movType;

    public float weaponCooldown = 2f;
    public ShootingPattern shootingPattern;
    public float projectileOffsetY = 0.4f;
    private GameObject projectilePrefab;
    private float cooldownTimer = 0;
    public ProjectileType projectileType;

    private float movChangeTimer = 2f;

    private GameState gameState;
    private GameObject player;
    private float screenWidth;

    private Health enemyHealth;
    private GameObject particleExplosion;

    private enum AnimationState
    {
        Idle,
        Left,
        Right
    }

    private AnimationState currentAnim = AnimationState.Idle;

    public enum MovType
    {
        Straight,
        Right,
        Left,
        TowardsPlayer
    }

    public enum ShootingPattern
    {
        Straight,
        Double,
        Cone,
        Triple
    }

    public enum ProjectileType
    {
        Red,
        Green,
        Blue
    }

    private GameObject dropHealthPrefab, dropShieldsPrefab, dropMissilesPrefab;

    private void Start()
    {
        if (gameState == null)
            gameState = GameObject.FindObjectOfType<GameState>();

        enemyHealth = gameObject.GetComponent<Health>();

        screenWidth = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
        player = GameObject.FindGameObjectWithTag("Player");

        particleExplosion = (GameObject)Resources.Load("Prefabs/ParticleExplosion", typeof(GameObject));

        switch (projectileType)
        {
            case ProjectileType.Blue:
                projectilePrefab = (GameObject)Resources.Load("Prefabs/projectile04", typeof(GameObject));
                break;

            case ProjectileType.Green:
                projectilePrefab = (GameObject)Resources.Load("Prefabs/projectile03", typeof(GameObject));
                break;

            case ProjectileType.Red:
            default:
                projectilePrefab = (GameObject)Resources.Load("Prefabs/projectile02", typeof(GameObject));
                break;
        }

        dropHealthPrefab = (GameObject)Resources.Load("Prefabs/Drops/drop_health", typeof(GameObject));
        dropShieldsPrefab = (GameObject)Resources.Load("Prefabs/Drops/drop_shields", typeof(GameObject));
        dropMissilesPrefab = (GameObject)Resources.Load("Prefabs/Drops/drop_missiles", typeof(GameObject));

        projectilePrefab.layer = 11;
    }

    private void RandomizeMovType()
    {
        movType = (MovType)Random.Range(0, 3);
    }

    private void Update()
    {
        Move();
        Shoot();

        if (enemyHealth.currentHealth <= 0)
        {
            GameObject explo = Instantiate(particleExplosion, transform.position, transform.rotation);
            Destroy(explo, 2f);
        }
    }

    private void LateUpdate()
    {
        if (enemyHealth.currentHealth <= 0)
        {
            int rand = Random.Range(0, 100);
            if (rand < 20)
                Instantiate(dropHealthPrefab, transform.position, transform.rotation);
            else if (rand < 40)
                Instantiate(dropShieldsPrefab, transform.position, transform.rotation);
            else if (rand < 60)
                Instantiate(dropMissilesPrefab, transform.position, transform.rotation);

            Destroy(gameObject);
            gameState.ScorePoints(points);
            gameState.enemyKilled++;
        }
    }

    private void Shoot()
    {
        cooldownTimer -= Time.deltaTime;

        // shoot
        if (cooldownTimer <= 0)
        {
            if (shootingPattern == ShootingPattern.Triple)
                ShootTriple();
            else if (shootingPattern == ShootingPattern.Cone)
                ShootCone();
            else if (shootingPattern == ShootingPattern.Double)
                ShootDouble();
            else
                ShootStraight();

            cooldownTimer = weaponCooldown;
        }
    }

    private void ShootStraight()
    {
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y - projectileOffsetY), transform.rotation);
        projectile.tag = "Projectile";
    }

    private void ShootDouble()
    {
        GameObject projectile1 = Instantiate(projectilePrefab, new Vector3(transform.position.x - 0.2f, transform.position.y - projectileOffsetY), transform.rotation);
        projectile1.tag = "Projectile";
        GameObject projectile2 = Instantiate(projectilePrefab, new Vector3(transform.position.x + 0.2f, transform.position.y - projectileOffsetY), transform.rotation);
        projectile2.tag = "Projectile";
    }

    private void ShootCone()
    {
        GameObject projectile1 = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y - projectileOffsetY), transform.rotation);
        projectile1.transform.Rotate(new Vector3(0, 0, 5f));
        projectile1.tag = "Projectile";
        GameObject projectile2 = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y - projectileOffsetY), transform.rotation);
        projectile2.transform.Rotate(new Vector3(0, 0, -5f));
        projectile2.tag = "Projectile";
    }

    private void ShootTriple()
    {
        ShootStraight();
        ShootCone();
    }

    private void Move()
    {
        movChangeTimer -= Time.deltaTime;

        // change randomly
        if (movChangeTimer <= 0 && movType != MovType.TowardsPlayer)
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
        if (currentAnim != AnimationState.Right)
        {
            GetComponent<Animator>().SetTrigger("Tilt");
            transform.localScale = new Vector3(1, 1, 1);
            currentAnim = AnimationState.Right;
        }
        transform.Translate(new Vector3(-speed / 2 * Time.deltaTime, speed * Time.deltaTime, 0));
    }

    private void GoDiagonalLeft()
    {
        if (currentAnim != AnimationState.Left)
        {
            GetComponent<Animator>().SetTrigger("Tilt");
            transform.localScale = new Vector3(-1, 1, 1);
            currentAnim = AnimationState.Left;
        }
        transform.Translate(new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0));
    }

    private void GoStraight()
    {
        if (currentAnim != AnimationState.Idle)
        {
            GetComponent<Animator>().SetTrigger("Idle");
            currentAnim = AnimationState.Idle;
        }
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}