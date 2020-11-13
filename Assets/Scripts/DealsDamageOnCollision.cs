using UnityEngine;

public class DealsDamageOnCollision : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.GetComponent<Health>() != null)
        {
            //Debug.Log("Deals " + damage + " damage: " + gameObject + " to " + collision);
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        else
            Debug.LogWarning("Collided with an Object without Health component!");
    }
}