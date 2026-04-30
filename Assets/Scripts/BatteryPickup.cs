using UnityEngine;
using TMPro;

public class BatteryPickup : MonoBehaviour
{
    [Header("Battery settings")]
    public float batteryAmount = 30f;   // koliko % doda ena baterija

    [Header("UI")]
    public TMP_Text PickUpText;

    private void Start()
    {
        if (PickUpText != null)
            PickUpText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (PickUpText != null) PickUpText.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Najdi flashlight na playerju
            Flashlight fl = other.GetComponentInChildren<Flashlight>();

            if (fl == null)
            {
                Debug.LogWarning("Flashlight ni najden na Playerju!");
                return;
            }

            // napolni baterijo
            fl.AddBattery(batteryAmount);

            // skrij UI
            if (PickUpText != null)
                PickUpText.gameObject.SetActive(false);

            // izbriši baterijo iz sveta
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (PickUpText != null) PickUpText.gameObject.SetActive(false);
    }
}