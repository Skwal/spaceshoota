using UnityEngine;

public class Shields : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();
    }

    private void LateUpdate()
    {
        gameObject.SetActive(health.currentHealth > 0);
    }
}