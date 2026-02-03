using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Image icon;
    public GameObject item;

    Vector3 normalScale;
    Vector3 selectedScale;

    void Awake()
    {
        normalScale = Vector3.one;
        selectedScale = Vector3.one * 1.15f;
    }

    public void SetItem(GameObject newItem, Sprite newIcon)
    {
        item = newItem;
        icon.sprite = newIcon;
        icon.enabled = true;
    }

    public void SetSelected(bool selected)
    {
        transform.localScale = selected ? selectedScale : normalScale;
    }
}
