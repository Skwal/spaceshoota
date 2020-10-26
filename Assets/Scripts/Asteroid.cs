using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotSpeed = 50f;

    private Health health;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();
    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
        if (health.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}