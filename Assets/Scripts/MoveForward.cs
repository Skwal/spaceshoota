using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
