using UnityEngine;

public class ExplodesOnDeath : MonoBehaviour
{
    public float scaleSprite = 12f;
    public GameObject explosionPrefab;

    private float cooldown = 0;

    private void Update()
    {
        if (gameObject.GetComponent<Health>().currentHealth == 0 && cooldown <= 0)
        {
            Explode();
            cooldown = 5f;
        }

        cooldown -= Time.deltaTime;
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        explosion.transform.localScale *= scaleSprite;

        float delay = explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosion, delay);
    }
}