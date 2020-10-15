using UnityEngine;

public class DestroyOutOfScreen : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -Camera.main.orthographicSize - 2 || transform.position.y > Camera.main.orthographicSize + 2)
        {
            Destroy(gameObject);
        }
    }
}