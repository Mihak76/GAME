using UnityEngine;
using UnityEngine.EventSystems;

public class GlowHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject glow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        glow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        glow.SetActive(false);
    }
}