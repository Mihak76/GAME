using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    public GameObject itemOnPlayer; // Flashlight ali Glock na roki
    public TMP_Text PickUpText;

    void Start()
    {
        PickUpText.gameObject.SetActive(false);
        itemOnPlayer.SetActive(false); // na začetku skrit
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PickUpText.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PickUpText.gameObject.SetActive(false);

            // Poišči ItemsManager
            ItemsManager im = other.GetComponentInChildren<ItemsManager>();

            // Dodaj **samo model na Playerju**, ne tisti na tleh
            im.PickUpItem(itemOnPlayer);

            // skrij model na tleh
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            PickUpText.gameObject.SetActive(false);
    }
}
