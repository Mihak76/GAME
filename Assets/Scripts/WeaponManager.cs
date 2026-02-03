using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [Header("Active Weapon Slot")]
    public Transform weaponSpawn; // WeaponSpawn iz Hierarchy

    [Header("Drop Settings")]
    public KeyCode dropKey = KeyCode.G; // Nastavljiva tipka za drop

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
        weaponObject.transform.SetParent(weaponSpawn, false);

        // Snap to WeaponSpawn
        weaponObject.transform.localPosition = Vector3.zero;
        weaponObject.transform.localRotation = Quaternion.identity;

        currentWeapon = weaponObject;

        // Disable physics pri pickup-u
        Rigidbody rb = weaponObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
            rb.useGravity = false;
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

        // Odparentaj weapon
        currentWeapon.transform.SetParent(null);

        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Omogoči physics in gravitacijo
            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.useGravity = true;

            // Dodaj rahlo naključno rotacijo, da weapon pade realistično
            rb.AddTorque(
                new Vector3(
                    Random.Range(-150f, 150f),
                    Random.Range(-150f, 150f),
                    Random.Range(-150f, 150f)
                ),
                ForceMode.Impulse
            );

            // Dodaj rahlo silo naprej in navzgor
            rb.AddForce(currentWeapon.transform.forward * 1f + Vector3.up * 0.5f, ForceMode.Impulse);
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