using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [Header("Disable when inventory open (povleci SEM vse skripte, ki premikajo/obračajo)")]
    public MonoBehaviour[] disableWhenInventoryOpen;

    [Header("Hotbar (4)")]
    public HotbarSlot[] hotbarSlots;

    [Header("Inventory (grid)")]
    public HotbarSlot[] inventorySlots;

    [Header("Inventory UI")]
    public GameObject inventoryPanel;

    int currentIndex = -1;
    bool itemHidden = false;
    bool inventoryOpen = false;

    void Start()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        inventoryOpen = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        UpdateHotbarSelection();
        RefreshHeldItemVisibility();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleInventory();

        if (inventoryOpen) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectHotbarSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectHotbarSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectHotbarSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectHotbarSlot(3);

        if (Input.GetKeyDown(KeyCode.X))
        {
            itemHidden = !itemHidden;
            RefreshHeldItemVisibility();
        }
    }

    void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;

        if (inventoryPanel != null)
            inventoryPanel.SetActive(inventoryOpen);

        Cursor.visible = inventoryOpen;
        Cursor.lockState = inventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;

        // Zelo pomembno: reset mouse delta, da ne "odnese" igralca
        Input.ResetInputAxes();

        // Izklopi vse skripte, ki obračajo / premikajo igralca
        if (disableWhenInventoryOpen != null)
        {
            for (int i = 0; i < disableWhenInventoryOpen.Length; i++)
            {
                if (disableWhenInventoryOpen[i] != null)
                    disableWhenInventoryOpen[i].enabled = !inventoryOpen;
            }
        }

        if (inventoryOpen) HideAllHeldItems();
        else RefreshHeldItemVisibility();
    }

    public void PickUpItem(GameObject itemOnPlayer, Sprite icon)
    {
        if (itemOnPlayer == null)
        {
            Debug.LogWarning("PickUpItem: itemOnPlayer je NULL!");
            return;
        }

        itemOnPlayer.SetActive(false);

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (hotbarSlots[i].IsEmpty)
            {
                hotbarSlots[i].SetItem(itemOnPlayer, icon);
                SelectHotbarSlot(i);
                return;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].IsEmpty)
            {
                inventorySlots[i].SetItem(itemOnPlayer, icon);
                RefreshHeldItemVisibility();
                return;
            }
        }

        Debug.Log("Hotbar + Inventory sta polna!");
    }

    void SelectHotbarSlot(int index)
    {
        if (index < 0 || index >= hotbarSlots.Length) return;
        if (hotbarSlots[index].IsEmpty) return;

        currentIndex = index;
        UpdateHotbarSelection();
        RefreshHeldItemVisibility();
    }

    void UpdateHotbarSelection()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            bool selected = (i == currentIndex && currentIndex != -1);
            hotbarSlots[i].SetSelected(selected);
        }
    }

    void HideAllHeldItems()
    {
        foreach (var s in hotbarSlots)
            if (!s.IsEmpty) s.item.SetActive(false);

        foreach (var s in inventorySlots)
            if (!s.IsEmpty) s.item.SetActive(false);
    }

    void RefreshHeldItemVisibility()
    {
        if (inventoryOpen)
        {
            HideAllHeldItems();
            return;
        }

        HideAllHeldItems();

        if (currentIndex != -1 && !itemHidden && !hotbarSlots[currentIndex].IsEmpty)
            hotbarSlots[currentIndex].item.SetActive(true);
    }
}