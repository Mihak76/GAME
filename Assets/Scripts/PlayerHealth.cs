using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject hud;
    public GameObject inv;
    public GameObject deathScreen;
    public GameObject player;
    public GameObject bloodImage;

    public float maxHealth = 100f;
    public float health;

    private bool isDead = false;

    void Start()
    {
        health = maxHealth;
        deathScreen.SetActive(false);
        bloodImage.SetActive(false);
    }

    // FUNKCIJA ZA DAMAGE
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;

        // Pokaži blood effect
        bloodImage.SetActive(true);
        Invoke("HideBlood", 0.3f);

        if (health <= 0)
        {
            Die();
        }
    }

    void HideBlood()
    {
        bloodImage.SetActive(false);
    }

    void Die()
    {
        isDead = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        hud.SetActive(false);
        inv.SetActive(false);
        deathScreen.SetActive(true);

        // Če želiš onemogočiti movement:
        // player.GetComponent<FirstPersonController>().enabled = false;
    }

    // Če želiš heal funkcijo
    public void Heal(float amount)
    {
        if (isDead) return;

        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
