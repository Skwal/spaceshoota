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

        // force type
        // type = 3;

        switch (type)
        {
            case 3:
                enemy = (GameObject)Resources.Load("Prefabs/asteroid0" + Random.Range(1, 4), typeof(GameObject));
                break;
            case 2:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship03", typeof(GameObject));
                break;
            case 1:
            default:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship01", typeof(GameObject));
                enemy.tag = "Enemy";
                break;
        }

        SpriteRenderer enemySprite = enemy.GetComponent<SpriteRenderer>() == null ? enemy.GetComponentInChildren<SpriteRenderer>() : enemy.GetComponent<SpriteRenderer>();

        float enemyHeight = enemySprite.bounds.size.y;

        enemySprite.sortingLayerName = "GameElements";

        Quaternion q = Quaternion.Euler(new Vector3(0, 0, 180));

        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(Random.Range(-4.0f, 4.0f), 5 + enemyHeight / 2, 0), q);

        // rotate asteroids slightly
        if (type == 3)
        {
            spawnedEnemy.transform.Rotate(new Vector3(0, 0, Random.Range(-20f, 20f)));
        }
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy(Random.Range(1, 4));
            spawnTimer = Random.Range(3, 6);
        }
    }
}