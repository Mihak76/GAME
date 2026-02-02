using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }  // <-- removed extra semicolon

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PickUpWeapon(GameObject PickedUpWeapon)
    {
        Destroy(PickedUpWeapon);
    }
}