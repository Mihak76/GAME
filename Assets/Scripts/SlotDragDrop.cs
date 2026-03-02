using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("References (lahko pustiš prazno, najde samo)")]
    public HotbarSlot slot;     // HotbarSlot na istem objektu (Slot1 / InvSlot)
    public Image dragIcon;      // DragIcon (ghost slika)
    public Canvas canvas;       // Canvas

    private CanvasGroup cg;

    void Awake()
    {
        if (slot == null) slot = GetComponent<HotbarSlot>();
        if (canvas == null) canvas = GetComponentInParent<Canvas>();

        cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();

        // DragIcon nikoli ne sme blokirat raycasta
        if (dragIcon != null)
            dragIcon.raycastTarget = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot == null || slot.IsEmpty) return;
        if (dragIcon == null) return;

        dragIcon.sprite = slot.icon != null ? slot.icon.sprite : null;
        dragIcon.enabled = true;
        dragIcon.gameObject.SetActive(true);

        // da med dragom ta slot ne blokira UI raycasta
        cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon == null || !dragIcon.gameObject.activeSelf) return;
        dragIcon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // skrij ghost
        if (dragIcon != null)
        {
            dragIcon.gameObject.SetActive(false);
            dragIcon.enabled = false;
            dragIcon.sprite = null;
        }

        cg.blocksRaycasts = true;

        // --- KLJUČ: ročno poišči slot pod miško in swap ---
        var targetSlotDD = FindSlotUnderPointer(eventData);
        if (targetSlotDD == null) return;                 // spustil si v prazno
        if (targetSlotDD == this) return;                 // spustil si na samega sebe
        if (slot == null || slot.IsEmpty) return;         // nič za premaknit

        Swap(slot, targetSlotDD.slot);

        // osveži item na roki (če imaš to funkcijo v ItemsManager)
        var im = FindFirstObjectByType<ItemsManager>();
        if (im != null)
            im.SendMessage("RefreshHeldItemVisibility", SendMessageOptions.DontRequireReceiver);
    }

    private SlotDragDrop FindSlotUnderPointer(PointerEventData eventData)
    {
        if (EventSystem.current == null) return null;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // poišči prvi UI element pod miško, ki ima SlotDragDrop (na sebi ali parentu)
        for (int i = 0; i < results.Count; i++)
        {
            var go = results[i].gameObject;
            var dd = go.GetComponentInParent<SlotDragDrop>();
            if (dd != null && dd.slot != null)
                return dd;
        }

        return null;
    }

    private void Swap(HotbarSlot a, HotbarSlot b)
    {
        if (a == null || b == null) return;

        GameObject aItem = a.item;
        Sprite aSprite = (a.icon != null) ? a.icon.sprite : null;

        GameObject bItem = b.item;
        Sprite bSprite = (b.icon != null) ? b.icon.sprite : null;

        a.SetItem(bItem, bSprite);
        b.SetItem(aItem, aSprite);
    }
}