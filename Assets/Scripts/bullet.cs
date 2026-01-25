using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
{
    if (objectWeHit.gameObject.CompareTag("Target"))
    {
        void CreateBulletImpactEffect(objectWeHit);
        print("hit " + objectWeHit.gameObject.name + " !");
        Destroy(gameObject);
    }
}

void CreateBulletImpactEffect(Collision objectWeHit)
{
    ContactPoint contact = objectWeHit.contact[0];
    GameObject hole = Instantiate(
    GlobalReference.Instance.bulletImpactEffectPrefab,
    contact.point,
    Quaternion.LookRotation(contact.normal)
    );
   

    hole.transform.SetParent(objectWeHit.gameObject.transform);
}
}
