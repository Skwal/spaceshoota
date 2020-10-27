using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed = 50f;

    private void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}