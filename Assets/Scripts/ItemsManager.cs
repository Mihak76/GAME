using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public HotbarSlot[] slots;
    int currentIndex = -1;
    bool itemHidden = false;

    void Start()
    {
        UpdateSelection();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);

        if (Input.GetKeyDown(KeyCode.X))
        {
            itemHidden = !itemHidden;
            UpdateSelection();
        }
    }

    public void PickUpItem(GameObject item, Sprite icon)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                Debug.Log("Dodajam item v slot " + i + " | Icon: " + icon);
                    slots[i].SetItem(item, icon);

                SelectSlot(i);
                return;
            }
        }

        Debug.Log("Hotbar je poln!");
    }

void SelectSlot(int index)
{
    if (index < 0 || index >= slots.Length) return;
    if (slots[index].item == null) return;

    currentIndex = index;
    UpdateSelection();
}

void UpdateSelection()
{
    for (int i = 0; i < slots.Length; i++)
    {
        bool selected = (i == currentIndex && currentIndex != -1);


        slots[i].SetSelected(selected);

        if (slots[i].item != null)
            slots[i].item.SetActive(selected && !itemHidden);
    }
}

}
