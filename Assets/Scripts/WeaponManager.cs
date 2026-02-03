using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [Header("Active Weapon Slot")]
    public Transform weaponSpawn; // ← povlečeš WeaponSpawn iz Hierarchy

    [Header("Drop Settings")]
    public KeyCode dropKey = KeyCode.G; // ← nastavljiva tipka za drop

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
        // DROP weapon po nastavljivi tipki
        if (currentWeapon != null && Input.GetKeyDown(dropKey))
        {
            DropWeapon();
        }
    }

    // =======================
    // PICK UP WEAPON
    // =======================
    public void PickUpWeapon(GameObject weaponObject)
    {
        if (weaponObject == null) return;

        // DROP trenutni weapon, če obstaja
        if (currentWeapon != null)
            DropWeapon();

        // Parent weapon na WeaponSpawn in uporabi lokalno pozicijo
        weaponObject.transform.SetParent(weaponSpawn, false); // ← ključna sprememba

        // Snap to WeaponSpawn (opcijsko, ker SetParent false že naredi lokalno pozicijo)
        weaponObject.transform.localPosition = Vector3.zero;
        weaponObject.transform.localRotation = Quaternion.identity;

        currentWeapon = weaponObject;

        // Disable physics
        Rigidbody rb = weaponObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        Collider col = weaponObject.GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Weapon w = weaponObject.GetComponent<Weapon>();
        if (w != null)
            w.isActiveWeapon = true;
    }

    // =======================
    // DROP WEAPON
    // =======================
    public void DropWeapon()
    {
        if (currentWeapon == null) return;

        currentWeapon.transform.SetParent(null);

        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
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