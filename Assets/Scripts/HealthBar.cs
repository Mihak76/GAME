using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBarIM;
    public float CurrentHealth;
    private float MaxHealth = 100f;
    private PlayerHealth player;

    void Start()
    {
        healthBarIM = GetComponent<Image>();

        // Updated line to remove obsolete warning
        player = Object.FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (player != null) // safety check in case player isn't found
        {
            CurrentHealth = player.health;
            healthBarIM.fillAmount = CurrentHealth / MaxHealth;
        }
    }
}
