using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    public Sprite icon;

    [Header("Player item (model na roki)")]
    public GameObject itemOnPlayer;

    [Header("UI")]
    public TMP_Text PickUpText;

    [Header("Look check")]
    public Camera playerCamera;
    public float pickupDistance = 3f;

    void Start()
    {
        if (PickUpText != null) PickUpText.gameObject.SetActive(false);
        if (itemOnPlayer != null) itemOnPlayer.SetActive(false);

        if (playerCamera == null) playerCamera = Camera.main;
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
          //  if (!IsLookingAtThisItem())
           // {
           //     Debug.Log("Ne gledaš v item (raycast fail).");
            //    return;
           // }

            if (itemOnPlayer == null)
            {
                Debug.LogWarning($"{name}: itemOnPlayer ni nastavljen v Inspectorju!");
                return;
            }

            ItemsManager im = other.GetComponentInChildren<ItemsManager>();
            if (im == null)
            {
                Debug.LogWarning("ItemsManager ni najden na Playerju!");
                return;
            }

            if (PickUpText != null) PickUpText.gameObject.SetActive(false);

            // poskusi vzeti ikono iz ItemData, sicer uporabi icon iz PickUp
            Sprite useIcon = icon;
            var data = itemOnPlayer.GetComponent<ItemData>();
            if (data != null && data.icon != null) useIcon = data.icon;

            Debug.Log("Pickup called!");
            im.PickUpItem(itemOnPlayer, useIcon);

            // skrij world item
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (PickUpText != null) PickUpText.gameObject.SetActive(false);
    }

    private bool IsLookingAtThisItem()
    {
        if (playerCamera == null) return true;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
        {
            // robustno: dela ne glede na parent/child collider setup
            return hit.collider.GetComponentInParent<PickUp>() == this;
        }
        return false;
    }
}