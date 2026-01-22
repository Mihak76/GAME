using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public float damage = 25f;
    public float damageInterval = 2f;

    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            StartCoroutine(DamageOverTime());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator DamageOverTime()
    {
        while (playerInRange)
        {
            playerHealth.health -= damage;
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
