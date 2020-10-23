using UnityEngine;

public class DestroyOutOfScreen : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -Camera.main.orthographicSize - 1 || transform.position.y > Camera.main.orthographicSize + 1)
        {
            Destroy(gameObject);
        }
    }
}