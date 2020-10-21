using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float imgHeight;
    private float posY;
    public float parallaxSpeed;

    private void Start()
    {
        posY = transform.position.y;
        imgHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, -parallaxSpeed * Time.deltaTime, 0));

        if (transform.position.y <= posY - imgHeight)
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}