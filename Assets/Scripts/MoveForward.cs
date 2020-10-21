using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}