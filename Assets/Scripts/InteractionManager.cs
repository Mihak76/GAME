using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [Header("Interaction Settings")]
    [SerializeField] private float interactDistance = 3f;

    private Weapon hoveredWeapon;

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
        HandleRaycast();
        HandlePickup();
    }

    // ===============================
    // RAYCAST ZA HOVER
    // ===============================
    private void HandleRaycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Weapon weapon = null;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            weapon = hit.transform.GetComponentInParent<Weapon>();
        }

        if (weapon != hoveredWeapon)
        {
            ClearOutline();
            hoveredWeapon = weapon;

            if (hoveredWeapon != null)
                EnableOutline(hoveredWeapon);
        }
    }

    // ===============================
    // PICKUP INPUT
    // ===============================
    private void HandlePickup()
    {
        if (hoveredWeapon != null && Input.GetKeyDown(KeyCode.E))
        {
            // Pokliči Weapon.PickUp (WeaponManager spawn)
            hoveredWeapon.PickUp(WeaponManager.Instance.weaponSpawn);

            ClearOutline();
        }
    }

    // ===============================
    // ENABLE OUTLINE
    // ===============================
    private void EnableOutline(Weapon weapon)
    {
        Outline outline = weapon.GetComponentInChildren<Outline>();
        if (outline != null)
            outline.enabled = true;
    }

    // ===============================
    // CLEAR OUTLINE
    // ===============================
    private void ClearOutline()
    {
        if (hoveredWeapon == null) return;

        Outline outline = hoveredWeapon.GetComponentInChildren<Outline>();
        if (outline != null)
            outline.enabled = false;

        hoveredWeapon = null;
    }
}