using UnityEngine;

public class TekočaVoda : MonoBehaviour
{
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.0f;
    private Material mat;

    void Start()
    {
        // Shranimo referenco, da se ne ustvarja kopija vsak frame
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        mat.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
