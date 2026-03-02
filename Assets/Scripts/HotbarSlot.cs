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

        // AUTO-WIRE: če icon ni nastavljen, naj najde child "Icon"
        if (icon == null)
        {
            Transform t = transform.Find("Icon");
            if (t != null) icon = t.GetComponent<Image>();
        }

        if (icon != null) icon.enabled = (item != null);
        else Debug.LogWarning($"[{name}] HotbarSlot nima nastavljenega 'icon' Image!");
    }

    public bool IsEmpty => item == null;

    public void SetItem(GameObject newItem, Sprite newIcon)
    {
        item = newItem;

        if (icon != null)
        {
            icon.sprite = newIcon;
            icon.enabled = (newItem != null);
        }
    }

    public void Clear()
    {
        item = null;
        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public void SetSelected(bool selected)
    {
        transform.localScale = selected ? selectedScale : normalScale;
    }
}