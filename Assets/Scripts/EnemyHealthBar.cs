using UnityEngine;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public TextMeshProUGUI healthText;

    int maxHealth;

    void Start()
    {
        maxHealth = enemyHealth.health;
    }

    void Update()
    {
        healthText.text = enemyHealth.health + " / " + maxHealth;
    }
}