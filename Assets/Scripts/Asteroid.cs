using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float points = 5f;

    private Health health;
    private GameState gameState;

    private GameObject particleExplosion;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();
        gameState = GameObject.FindObjectOfType<GameState>();

        particleExplosion = (GameObject)Resources.Load("Prefabs/ParticleExplosion", typeof(GameObject));
    }

    private void Update()
    {
        if (health.currentHealth <= 0)
        {
            GameObject explo = Instantiate(particleExplosion, transform.position, transform.rotation);
            Destroy(explo, 2f);
        }
    }

    private void LateUpdate()
    {
        if (health.currentHealth <= 0)
        {
            Destroy(gameObject);
            gameState.ScorePoints(points);
            gameState.objectsDestroyed++;
        }
    }
}