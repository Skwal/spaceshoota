using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{
    private Health health;

    void Start()
    {
        health = gameObject.GetComponent<Health>();
    }

    private void LateUpdate()
    {
        if (health.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
