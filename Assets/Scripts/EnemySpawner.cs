using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameState gameState;
    private float spawnTimer = 2;

    private void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
    }

    private void SpawnEnemy(int type)
    {
        GameObject enemy;
        float rotation = 180f;

        switch (type)
        {
            case 7:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship02", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootingPattern = EnemyController.ShootingPattern.Cone;
                enemy.GetComponent<EnemyController>().weaponCooldown = 1f;
                enemy.GetComponent<EnemyController>().movType = EnemyController.MovType.TowardsPlayer;
                enemy.GetComponent<EnemyController>().projectileType = EnemyController.ProjectileType.Green;
                enemy.GetComponent<Health>().maxHealth = 30;
                enemy.tag = "Enemy";
                break;

            case 6:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship01", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootingPattern = EnemyController.ShootingPattern.Straight;
                enemy.GetComponent<EnemyController>().weaponCooldown = 2f;
                enemy.GetComponent<EnemyController>().movType = EnemyController.MovType.TowardsPlayer;
                enemy.GetComponent<EnemyController>().projectileType = EnemyController.ProjectileType.Red;
                enemy.GetComponent<Health>().maxHealth = 20;
                enemy.tag = "Enemy";
                break;

            case 5:
                enemy = (GameObject)Resources.Load("Prefabs/asteroid0" + Random.Range(1, 4), typeof(GameObject));
                enemy.GetComponent<MoveForward>().speed = 4f;
                rotation = Random.Range(160f, 200f);
                break;

            case 4:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship04", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootingPattern = EnemyController.ShootingPattern.Triple;
                enemy.GetComponent<EnemyController>().weaponCooldown = 4f;
                enemy.GetComponent<EnemyController>().movType = EnemyController.MovType.TowardsPlayer;
                enemy.GetComponent<EnemyController>().projectileType = EnemyController.ProjectileType.Blue;
                enemy.tag = "Enemy";
                break;

            case 3:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship03", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootingPattern = EnemyController.ShootingPattern.Double;
                enemy.GetComponent<EnemyController>().weaponCooldown = 3f;
                enemy.GetComponent<EnemyController>().movType = EnemyController.MovType.TowardsPlayer;
                enemy.GetComponent<EnemyController>().projectileType = EnemyController.ProjectileType.Red;
                enemy.tag = "Enemy";
                break;

            case 2:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship02", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootingPattern = EnemyController.ShootingPattern.Cone;
                enemy.GetComponent<EnemyController>().weaponCooldown = 2.5f;
                enemy.GetComponent<EnemyController>().movType = (EnemyController.MovType)Random.Range(0, 3);
                enemy.GetComponent<EnemyController>().projectileType = EnemyController.ProjectileType.Green;
                enemy.tag = "Enemy";
                break;

            case 1:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship01", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootingPattern = EnemyController.ShootingPattern.Straight;
                enemy.GetComponent<EnemyController>().movType = (EnemyController.MovType)Random.Range(0, 3);
                enemy.GetComponent<EnemyController>().projectileType = EnemyController.ProjectileType.Red;
                enemy.tag = "Enemy";
                break;

            case 0:
            default:
                enemy = (GameObject)Resources.Load("Prefabs/asteroid0" + Random.Range(1, 4), typeof(GameObject));
                rotation = Random.Range(160f, 200f);
                break;
        }


        SpriteRenderer enemySprite = enemy.GetComponent<SpriteRenderer>() == null ? enemy.GetComponentInChildren<SpriteRenderer>() : enemy.GetComponent<SpriteRenderer>();

        float enemyHeight = enemySprite.bounds.size.y;

        enemySprite.sortingLayerName = "GameElements";

        Quaternion q = Quaternion.Euler(new Vector3(0, 0, rotation));

        Instantiate(enemy, new Vector3(Random.Range(-4.0f, 4.0f), 5 + enemyHeight / 2, 0), q);
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            if (gameState.currentDifficulty >= 5)
            {
                SpawnEnemy(Random.Range(6, 8));
                spawnTimer = 1;
            }
            if (gameState.currentDifficulty == 4)
            {
                SpawnEnemy(Random.Range(5, 8));
                spawnTimer = Random.Range(2, 5);
            }
            if (gameState.currentDifficulty == 3)
            {
                SpawnEnemy(Random.Range(3, 6));
                spawnTimer = Random.Range(2, 4);
            }
            if (gameState.currentDifficulty == 2)
            {
                SpawnEnemy(Random.Range(2, 5));
                spawnTimer = Random.Range(3, 6);
            }
            else
            {
                SpawnEnemy(Random.Range(0, 3));
                spawnTimer = Random.Range(3, 6);
            }
        }
    }
}