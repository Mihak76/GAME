using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [Header("Weapon Spawn")]
    public Transform weaponSpawn; // WeaponSpawn iz Hierarchy

    [Header("Drop Settings")]
    public KeyCode dropKey = KeyCode.G;

    [Header("Hotbar")]
    public ItemsManager itemsManager; // reference na ItemsManager

    private GameObject currentWeapon;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (currentWeapon != null && Input.GetKeyDown(dropKey))
        {
            DropWeapon();
        }
    }

    // =======================
    // PICK UP WEAPON
    // =======================
    public void PickUpWeapon(GameObject weaponPrefab, Sprite icon)
    {
        if (weaponPrefab == null) return;

        // Instanciraj weapon za playerja
        GameObject weaponInstance = Instantiate(
            weaponPrefab,
            weaponSpawn.position,
            weaponSpawn.rotation,
            weaponSpawn
        );

        // Snap na WeaponSpawn
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localRotation = Quaternion.identity;

        // Naj bo skrit dokler ni izbran v hotbaru
        weaponInstance.SetActive(false);

        // Disable physics
        Rigidbody rb = weaponInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
            rb.useGravity = false;
        }

        Collider col = weaponInstance.GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        // Dodaj v hotbar
        if (itemsManager != null)
        {
            itemsManager.PickUpItem(weaponInstance, icon);
        }
        else
        {
            Debug.LogWarning("ItemsManager NI nastavljen v WeaponManager!");
        }

        currentWeapon = weaponInstance;

        Weapon w = weaponInstance.GetComponent<Weapon>();
        if (w != null)
            w.isActiveWeapon = true;
    }

    // =======================
    // DROP WEAPON
    // =======================
    public void DropWeapon()
    {
        if (currentWeapon == null) return;

        // Odparentaj weapon
        currentWeapon.transform.SetParent(null);

        // Enable physics
        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.useGravity = true;

            rb.AddTorque(
                new Vector3(
                    Random.Range(-150f, 150f),
                    Random.Range(-150f, 150f),
                    Random.Range(-150f, 150f)
                ),
                ForceMode.Impulse
            );

            rb.AddForce(
                currentWeapon.transform.forward * 1f + Vector3.up * 0.5f,
                ForceMode.Impulse
            );
        }

        Collider col = currentWeapon.GetComponent<Collider>();
        if (col != null)
            col.enabled = true;

        Weapon w = currentWeapon.GetComponent<Weapon>();
        if (w != null)
            w.isActiveWeapon = false;

        currentWeapon = null;
    }
}