using UnityEngine;
using TMPro;

public class MedkitUse : MonoBehaviour
{
    public GameObject player;
    public float addHealth = 25f;

    [Header("FX")]
    public AudioSource healSound;
    public GameObject screenFX;
    public TMP_Text cannotHealText;

    bool usingMedkit = false;

    void Start()
    {
        if (screenFX != null) screenFX.SetActive(false);
        if (cannotHealText != null) cannotHealText.gameObject.SetActive(false);
    }

    void Update()
    {
        // deluje SAMO ko je item aktiven v roki
        if (!gameObject.activeInHierarchy) return;

        if (Input.GetMouseButtonDown(1) && !usingMedkit) // DESNI KLIK
        {
            UseMedkit();
        }
    }

    void UseMedkit()
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();

        if (ph.health >= 100)
        {
            if (cannotHealText != null)
            {
                cannotHealText.gameObject.SetActive(true);
                Invoke(nameof(HideCannotHeal), 1.5f);
            }
            return;
        }

        usingMedkit = true;

        // HEAL
        ph.health += addHealth;
        ph.health = Mathf.Clamp(ph.health, 0, 100);

        // FX
        if (healSound != null) healSound.Play();
        if (screenFX != null) screenFX.SetActive(true);

        Invoke(nameof(FinishUsingMedkit), 1.2f);
    }

    void HideCannotHeal()
    {
        cannotHealText.gameObject.SetActive(false);
    }

 void FinishUsingMedkit()
{
    if (screenFX != null) screenFX.SetActive(false);

    // 🔥 najdi inventory
    ItemsManager im = player.GetComponentInChildren<ItemsManager>();

    if (im != null)
        im.RemoveItem(gameObject);

    // zdaj lahko uničimo objekt
    Destroy(gameObject);
}
}