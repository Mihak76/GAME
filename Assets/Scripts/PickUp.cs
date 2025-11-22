using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject FlashLightOnPlayer; // svetilka, ki se aktivira
    public GameObject PickUpText;         // tekst "Press E to pick up"

    void Start()
    {
        FlashLightOnPlayer.SetActive(false); // na začetku izklopljeno
        PickUpText.SetActive(false);         // tekst izklopljen
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.SetActive(true); // pokaže tekst, ko si ob svetilki

            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);  // pobereš svetilko s tal
                FlashLightOnPlayer.SetActive(true); // takoj se aktivira svetilka
                PickUpText.SetActive(false);        // skrije tekst
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUpText.SetActive(false); // ko odideš, skrije tekst
    }
}
