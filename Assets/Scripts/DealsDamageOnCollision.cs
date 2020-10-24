using UnityEngine;

public class DealsDamageOnCollision : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("Collided with an Object without Health component!");
        }
    }
}