using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float speed = 1.5f;
    public float lifeTime = 1f;

    TextMeshProUGUI text;
    float timer;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        // Dvigovanje gor
        transform.position += Vector3.up * speed * Time.deltaTime;

        // Timer
        timer += Time.deltaTime;

        // Fade out
        if (text != null)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / lifeTime);
            Color c = text.color;
            c.a = alpha;
            text.color = c;
        }

        // Uniči po času
        Destroy(gameObject, lifeTime);
    }
}
