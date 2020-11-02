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

        switch (type)
        {
            case 3:
                enemy = (GameObject)Resources.Load("Prefabs/asteroid0" + Random.Range(1, 3), typeof(GameObject));
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

        Instantiate(enemy, new Vector3(Random.Range(-4.0f, 4.0f), 5 + enemyHeight / 2, 0), q);
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy(Random.Range(1, 3));
            spawnTimer = Random.Range(3, 6);
        }
    }
}