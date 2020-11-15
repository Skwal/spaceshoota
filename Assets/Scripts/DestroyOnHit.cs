using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    private bool collided = false;

    private void LateUpdate()
    {
        if (collided)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
    }
}