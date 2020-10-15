using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameState gameState;
    float spawnTimer = 5;
        
    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
    }

    void SpawnEnemy(int type)
    {
        GameObject enemy;

        switch (type)
        {
            case 1:
            default:
                enemy = (GameObject)Resources.Load("Prefabs/enemy_ship01", typeof(GameObject));
                break;
        }

        SpriteRenderer enemySprite = enemy.GetComponent<SpriteRenderer>();
        enemySprite.sortingLayerName = "GameElements";

        Quaternion q = Quaternion.Euler(new Vector3(0, 0, 180));

        Instantiate(enemy, new Vector3(Random.Range(-4.0f, 4.0f), 6, 0), q);
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy(1);
            spawnTimer = Random.Range(3, 6);
        }
    }
}