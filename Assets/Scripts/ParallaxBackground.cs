using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    float imgHeight;
    float posY;
    public float parallaxSpeed;
    
    void Start()
    {
        posY = transform.position.y;
        imgHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    
    void Update()
    {
        transform.Translate(new Vector3(0, -parallaxSpeed * Time.deltaTime, 0));

        if (transform.position.y <= posY - imgHeight)
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}
