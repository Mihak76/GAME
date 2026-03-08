using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    enum LightFlickerType { simple, noise }

    [Header("Light References")]
    [SerializeField] Light pointLight;
    [SerializeField] Light spotLight;

    [Header("Exposed Light Variables")]
    [Tooltip("The light's intensity will take on the value set by this variable")]
    [SerializeField] float maxIntensity = 2f;

    [Tooltip("Max On Time is the maximum duration that the light can be 'On' for when in 'Simple' flicker mode.")]
    [SerializeField] float maxOnTime = 5f;

    [Tooltip("Max Off Time is the maximum duration that the light can be 'Off' for when in 'Simple' flicker mode.")]
    [SerializeField] float maxOffTime = 0.5f;

    [Tooltip("Min On Time is the minimum duration that the light can be 'On' for when in 'Simple' flicker mode.")]
    [SerializeField] float minOnTime = 1f;

    [Tooltip("Min Off Time is the minimum duration that the light can be 'Off' for when in 'Simple' flicker mode.")]
    [SerializeField] float minOffTime = 0.1f;

    [Header("Noise Flicker Settings")]
    [Tooltip("Simple is just turning off and on with the time constraints. Noise uses Perlin noise to flicker the light.")]
    [SerializeField] LightFlickerType lightFlickerType = LightFlickerType.simple;

    [Tooltip("Frequency determines how fast the light will flicker.")]
    [SerializeField, Range(0f, 10f)] float frequency = 1f;

    [Tooltip("Bottom Cutoff determines at what % the light will go from dim to off instantly.")]
    [SerializeField, Range(0f, 0.49f)] float bottomCutoff = 0.25f;

    [Tooltip("Top Cutoff determines at what % the light will go from dim to maximum intensity instantly.")]
    [SerializeField, Range(0.5f, 1f)] float topCutoff = 0.75f;

    float intensitySeed;
    float randomOnTime = 5f;
    float randomOffTime = 0.1f;
    float currentTime = 0f;
    bool isLightOn = true;

    void Start()
    {
        SetLightIntensity(maxIntensity);

        intensitySeed = Random.Range(0f, 1000f);
        randomOffTime = maxOffTime;
        randomOnTime = maxOnTime;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (lightFlickerType == LightFlickerType.simple)
            SimpleFlicker();
        else
            NoiseFlicker();
    }

    void SimpleFlicker()
    {
        if (isLightOn)
        {
            if (currentTime >= randomOnTime)
            {
                randomOffTime = Random.Range(minOffTime, maxOffTime);
                ToggleLight();
            }
        }
        else
        {
            if (currentTime >= randomOffTime)
            {
                randomOnTime = Random.Range(minOnTime, maxOnTime);
                ToggleLight();
            }
        }
    }

    void ToggleLight()
    {
        currentTime = 0f;
        isLightOn = !isLightOn;

        SetLightIntensity(isLightOn ? maxIntensity : 0f);
    }

    void NoiseFlicker()
    {
        float intensityNoise = Mathf.PerlinNoise(intensitySeed, Time.time * frequency);
        float intensity = maxIntensity * intensityNoise;

        if (intensity > maxIntensity * topCutoff)
            intensity = maxIntensity;
        else if (intensity < maxIntensity * bottomCutoff)
            intensity = 0f;

        SetLightIntensity(intensity);
    }

    void SetLightIntensity(float intensity)
    {
        if (pointLight != null)
            pointLight.intensity = intensity;

        if (spotLight != null)
            spotLight.intensity = intensity;
    }
}