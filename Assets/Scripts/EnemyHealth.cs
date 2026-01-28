using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;                 // maksimalni health Enemy-ja
    public int currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage! Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject); // uniči Enemy
    }
}
