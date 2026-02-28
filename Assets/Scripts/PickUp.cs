using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    public Sprite icon;

    public GameObject itemOnPlayer;   // Flashlight/Glock model na roki
    public TMP_Text PickUpText;

    [Header("Look check")]
    public Camera playerCamera;       // povleci Main Camera sem (ali pusti prazno)
    public float pickupDistance = 3f; // max razdalja raycasta

    void Start()
    {
        if (PickUpText != null) PickUpText.gameObject.SetActive(false);
        if (itemOnPlayer != null) itemOnPlayer.SetActive(false);

        if (playerCamera == null) playerCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            if (PickUpText != null) PickUpText.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        // Moraš gledat v ta item (raycast iz kamere)
        if (!IsLookingAtThisItem()) return;

        if (PickUpText != null) PickUpText.gameObject.SetActive(false);

        ItemsManager im = other.GetComponentInChildren<ItemsManager>();
        if (im != null && itemOnPlayer != null)
        {
            Debug.Log("Pickup called!");
            ItemData data = itemOnPlayer.GetComponent<ItemData>();
            if (data != null)
                im.PickUpItem(itemOnPlayer, data.icon);
            else
                im.PickUpItem(itemOnPlayer, icon); // fallback, če nima ItemData
        }

        // skrij model na tleh
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            if (PickUpText != null) PickUpText.gameObject.SetActive(false);
    }

    private bool IsLookingAtThisItem()
    {
        if (playerCamera == null) return true; // če ni kamere, ne blokiraj

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
        {
            // zadeti mora collider od tega itema ali katerega od childov
            return hit.collider.transform == transform || hit.collider.transform.IsChildOf(transform);
        }

        return false;
    }
}