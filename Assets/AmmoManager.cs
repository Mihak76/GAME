using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI ammoDisplay;

    [Header("Settings")]
    public int maxReserveAmmo = 120; // optional max ammo

    private int currentAmmo = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateAmmoUI();
    }

    // =============================
    // Dodaj ammo (AmmoBox pickup)
    // =============================
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxReserveAmmo)
            currentAmmo = maxReserveAmmo;

        UpdateAmmoUI();
    }

    // =============================
    // Vrni trenutno ammo količino
    // =============================
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    // =============================
    // Odstrani ammo (Reload)
    // =============================
    public void RemoveAmmo(int amount)
    {
        currentAmmo -= amount;
        if (currentAmmo < 0)
            currentAmmo = 0;

        UpdateAmmoUI();
    }

    // =============================
    // Porabi ammo pri strelu
    // =============================
    public bool UseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateAmmoUI();
            return true;
        }

        return false;
    }

    // =============================
    // Update UI
    // =============================
    public void UpdateAmmoUI(int magazineAmmo = -1)
    {
        if (ammoDisplay == null) return;

        if (magazineAmmo >= 0)
        {
            // Prikaz: magazine / reserve
            ammoDisplay.text = $"{magazineAmmo} / {currentAmmo}";
        }
        else
        {
            ammoDisplay.text = $"Reserve: {currentAmmo}";
        }
    }
}