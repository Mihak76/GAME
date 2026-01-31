using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Slider slider;
    public TextMeshProUGUI healthText;

    int maxHealth;

    void Start()
    {
        maxHealth = enemyHealth.health;
        slider.maxValue = maxHealth;
    }

    void Update()
    {
        slider.value = enemyHealth.health;
        healthText.text = enemyHealth.health + " / " + maxHealth;
    }
}
