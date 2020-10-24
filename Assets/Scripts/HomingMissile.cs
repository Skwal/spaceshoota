using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 4f;
    public float rotSpeed = 50f;

    public Transform nearestEnemy;
    private Health health;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();
    }

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            nearestEnemy = GetClosestEnemy(enemies);

            Vector3 dir = nearestEnemy.position - transform.position;
            dir.Normalize();

            float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
        }

        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }

    private void LateUpdate()
    {
        if (health.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }
}