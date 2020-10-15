using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // enemy properties
    public float speed = 1f;
    public float movType = 1f;
    public float weaponCooldown = 1f;
    public float health = 1f;
    public float points = 10f;

    public GameObject projectilePrefab;
    float cooldownTimer = 0;
       
    GameState gameState;   


    void Start()
    {
        projectilePrefab.layer = 9;
        if (gameState == null)
            gameState = GameObject.FindObjectOfType<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();


        if (health <= 0)
        {
            Destroy(gameObject);
            gameState.ScorePoints(points);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
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

    void setMovType(int type)
    {
        movType = type;
    }
}
