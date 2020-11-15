using UnityEngine;

public class Shields : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();
    }

    private void LateUpdate()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = health.currentHealth > 0;
        gameObject.GetComponent<CircleCollider2D>().enabled = health.currentHealth > 0;
    }
}