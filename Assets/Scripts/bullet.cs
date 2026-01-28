using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            CreateBulletImpactEffect(objectWeHit);
            Debug.Log("hit " + objectWeHit.gameObject.name + "!");
            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision collision)
    {
        // Example: just log for now
        Debug.Log("Bullet impact at " + collision.contacts[0].point);

        // Later you could do something like:
        // Instantiate(impactEffectPrefab, collision.contacts[0].point, Quaternion.identity);
    }
}
