using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;          // damage, ki ga bullet naredi
    public float lifeTime = 5f;      // bullet se uniči po 5 sekundah, če ne zadene

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;

        // Bullet uniči Target takoj
        if (hitObject.CompareTag("Target"))
        {
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
        // Bullet zadene Enemy
        else if (hitObject.CompareTag("Enemy"))
        {
            CreateBulletImpactEffect(collision);

            // Poišči EnemyHealth na objektu ali v parent
            EnemyHealth enemyHealth = hitObject.GetComponent<EnemyHealth>();
            if (enemyHealth == null)
            {
                enemyHealth = hitObject.GetComponentInParent<EnemyHealth>();
            }

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // bullet se uniči po enem zadetku
        }
        else
        {
            // Karkoli drugega (npr. zid)
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision collision)
    {
        Debug.Log("Bullet impact at " + collision.contacts[0].point + " on " + collision.gameObject.name);
    }
}
