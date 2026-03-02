using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance { get; set; }

    public TextMeshProUGUI ammoDisplay;

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

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        UpdateAmmoUI();
    }

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

    void UpdateAmmoUI()
    {
        ammoDisplay.text = "Ammo: " + currentAmmo;
    }
}