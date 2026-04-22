using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [Header("Hotbar movement")]
    public RectTransform hotbarPanel;
    public RectTransform hotbarClosedAnchor;
    public RectTransform hotbarOpenAnchor;
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
        MoveHotbarTo(hotbarClosedAnchor);
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
    Debug.Log("Inventory open = " + inventoryOpen);
    if (inventoryPanel != null)
        inventoryPanel.SetActive(inventoryOpen);

    Cursor.visible = inventoryOpen;
    Cursor.lockState = inventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;

    Input.ResetInputAxes();

    if (disableWhenInventoryOpen != null)
    {
        for (int i = 0; i < disableWhenInventoryOpen.Length; i++)
        {
            if (disableWhenInventoryOpen[i] != null)
                disableWhenInventoryOpen[i].enabled = !inventoryOpen;
        }
    }

    if (inventoryOpen)
        MoveHotbarTo(hotbarOpenAnchor);
    else
        MoveHotbarTo(hotbarClosedAnchor);

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
    void MoveHotbarTo(RectTransform targetAnchor)
{
    if (hotbarPanel == null || targetAnchor == null)
    {
        Debug.Log("MoveHotbarTo FAIL: manjka referenca");
        return;
    }

    Debug.Log("Premikam hotbar na: " + targetAnchor.name);

    hotbarPanel.SetParent(targetAnchor, false);
    hotbarPanel.anchoredPosition = Vector2.zero;
    hotbarPanel.localRotation = Quaternion.identity;
    hotbarPanel.localScale = Vector3.one;
}
// Doda to metodo v ItemsManager, da ItemUsage ve, kateri slot je izbran

}