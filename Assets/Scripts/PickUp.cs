using UnityEngine;
using TMPro; // TextMeshPro

public class PickUp : MonoBehaviour
{
    public GameObject FlashLightOnPlayer; 
    public TMP_Text PickUpText;  // TextMeshPro

    void Start()
    {
        FlashLightOnPlayer.SetActive(false);

        // Predgeneriramo tekst
        PickUpText.gameObject.SetActive(true);
        var color = PickUpText.color;
        color.a = 0f;
        PickUpText.color = color;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var color = PickUpText.color;
            color.a = 1f;
            PickUpText.color = color;

            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                FlashLightOnPlayer.SetActive(true);

                color.a = 0f;
                PickUpText.color = color;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var color = PickUpText.color;
        color.a = 0f;
        PickUpText.color = color;
    }
}
