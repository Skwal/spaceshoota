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

        // force type
        // type = 4;

        switch (type)
        {
            case 4:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship04", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootType = EnemyController.ShootType.Triple;
                enemy.GetComponent<EnemyController>().weaponCooldown = 4f;
                enemy.GetComponent<EnemyController>().movType = EnemyController.MovType.TowardsPlayer;
                break;

            case 3:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship03", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootType = EnemyController.ShootType.Double;
                enemy.GetComponent<EnemyController>().weaponCooldown = 3f;
                enemy.GetComponent<EnemyController>().movType = EnemyController.MovType.TowardsPlayer;
                break;

            case 2:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship02", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootType = EnemyController.ShootType.Cone;
                enemy.GetComponent<EnemyController>().weaponCooldown = 2.5f;
                enemy.GetComponent<EnemyController>().movType = (EnemyController.MovType)Random.Range(0, 3);
                break;

            case 1:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship01", typeof(GameObject));
                enemy.GetComponent<EnemyController>().shootType = EnemyController.ShootType.Straight;
                enemy.GetComponent<EnemyController>().movType = (EnemyController.MovType)Random.Range(0, 3);
                break;

            case 0:
            default:
                enemy = (GameObject)Resources.Load("Prefabs/asteroid0" + Random.Range(1, 4), typeof(GameObject));
                rotation = Random.Range(160f, 200f);
                break;
        }

        enemy.tag = "Enemy";

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
            SpawnEnemy(Random.Range(0, 5));
            spawnTimer = Random.Range(3, 6);
        }
    }
}