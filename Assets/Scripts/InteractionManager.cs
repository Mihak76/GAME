using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    private Weapon hoveredWeapon;

    [SerializeField] private float interactDistance = 3f;

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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Check for Weapon component on hit object or its parents
            Weapon weapon = hit.transform.GetComponentInParent<Weapon>();

            if (weapon != null)
            {
                if (hoveredWeapon != weapon)
                {
                    ClearOutline();
                    hoveredWeapon = weapon;
                    EnableOutline(hoveredWeapon);
                }
                return;
            }
        }

        // When not looking at a weapon
        ClearOutline();
    }

    private void EnableOutline(Weapon weapon)
    {
        Outline outline = weapon.GetComponentInChildren<Outline>();
        if (outline != null)
        {
            outline.enabled = true;

            // Pick up weapon when pressing E
            if (Input.GetKeyDown(KeyCode.E))
            {
                WeaponManager.Instance.PickUpWeapon(weapon.gameObject);
                hoveredWeapon = null; // Clear reference after pickup
            }
        }
    }

    private void ClearOutline()
    {
        if (hoveredWeapon == null) return;

        Outline outline = hoveredWeapon.GetComponentInChildren<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }

        hoveredWeapon = null;
    }
}