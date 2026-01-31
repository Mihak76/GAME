using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 5;
    public GameObject floatingTextPrefab;

    public void TakeDamage(int damage)
    {
        health -= damage;

        ShowFloatingText(damage);

        if (health <= 0)
        {
            Die();
        }
    }

    void ShowFloatingText(int damage)
    {
        if (floatingTextPrefab)
        {
            GameObject text = Instantiate(
                floatingTextPrefab,
                transform.position + Vector3.up * 2f,
                Quaternion.identity
            );

            text.transform.forward = Camera.main.transform.forward;

        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
