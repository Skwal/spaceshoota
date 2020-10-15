using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameState gameState;
        
    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(2);

        spawnEnemy(1);
        StartCoroutine(spawnEnemies());
    }

    void spawnEnemy(int type)
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

        EnemyController controller = enemy.GetComponent<EnemyController>();
        controller.movType = Random.Range(1, 4);

        Instantiate(enemy, new Vector3(Random.Range(-4.0f, 4.0f), 6, 0), q);
    }
}