using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    public GameObject FlashLightOnPlayer;
    public TMP_Text PickUpText;

    void Start()
    {
        FlashLightOnPlayer.SetActive(false);
        PickUpText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            FlashLightOnPlayer.SetActive(true);
            PickUpText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.gameObject.SetActive(false);
        }
    }
}
