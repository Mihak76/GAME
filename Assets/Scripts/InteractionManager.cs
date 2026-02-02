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
            // HĽADÁ WEAPON AJ NA PARENTOCH (dôležité!)
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

        // Keď mierime mimo zbrane alebo na nič
        ClearOutline();
    }

    private void EnableOutline(Weapon weapon)
    {
        Outline outline = weapon.GetComponentInChildren<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
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