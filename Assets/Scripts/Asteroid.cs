using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotSpeed = 50f;
    public float points = 5f;

    private Health health;
    private GameState gameState;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();
        gameState = GameObject.FindObjectOfType<GameState>();
    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
        if (health.currentHealth <= 0)
        {
            Debug.Log("Assteroid");
            Destroy(gameObject);
            gameState.ScorePoints(points);
            gameState.objectsDestroyed++;
        }
    }
}