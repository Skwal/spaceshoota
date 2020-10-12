using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public float movType = 1f;

    public GameObject projectilePrefab;
    public float weaponCooldown = 1f;
    float cooldownTimer = 0;

    public float health = 1f;
    // Start is called before the first frame update
    void Start()
    {
        projectilePrefab.layer = 9;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();


        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        health--;
    }

    void Shoot()
    {
        cooldownTimer -= Time.deltaTime;

        // shoot
        if (cooldownTimer <= 0)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            cooldownTimer = weaponCooldown;
        }
    }

    void Move()
    {
        if (movType == 2)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, speed * Time.deltaTime, 0));
        }
        else if (movType == 3)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0));
        }
        else
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }

    }
}
