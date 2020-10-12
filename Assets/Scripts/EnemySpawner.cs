using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameState gameState;
    
    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
    }

    void Update()
    {
        
    }
}
