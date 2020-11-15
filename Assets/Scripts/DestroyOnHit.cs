using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    private bool collided = false;

    private void Update()
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