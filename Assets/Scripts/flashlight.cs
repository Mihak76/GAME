using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    [Header("Flashlight Visuals")]
    public GameObject ON;
    public GameObject OFF;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    [Header("Particles")]
    public ParticleSystem dustParticles;

    [Header("Battery Settings")]
    public float maxBattery = 100f;
    public float currentBattery = 100f;
    public float drainPerSecond = 5f;

    [Header("Battery UI")]
    public TMP_Text batteryText;

    private bool isON = false;

    void Start()
    {
        // začetno stanje svetilke
        ON.SetActive(false);
        OFF.SetActive(true);

        if (dustParticles != null)
            dustParticles.Stop();

        UpdateBatteryUI();
    }

    void Update()
    {
        DrainBattery();
        UpdateBatteryUI();

        // Prižig / ugašanje svetilke
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Če ni baterije → ne moreš prižgati
            if (currentBattery <= 0f)
                return;

            ToggleFlashlight();
        }
    }

    // -----------------------------
    // 🔋 PRAZNJENJE BATERIJE
    // -----------------------------
    void DrainBattery()
    {
        if (!isON) return;
        if (currentBattery <= 0f) return;

        currentBattery -= drainPerSecond * Time.deltaTime;

        if (currentBattery <= 0f)
        {
            currentBattery = 0f;
            TurnOffFlashlight();
        }
    }

    // -----------------------------
    // 🔦 VKLOP / IZKLOP
    // -----------------------------
    void ToggleFlashlight()
    {
        isON = !isON;

        ON.SetActive(isON);
        OFF.SetActive(!isON);

        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        if (dustParticles != null)
        {
            if (isON && !dustParticles.isPlaying)
                dustParticles.Play();
            else if (!isON && dustParticles.isPlaying)
                dustParticles.Stop();
        }
    }

    // prisilni izklop ko zmanjka baterije
    void TurnOffFlashlight()
    {
        isON = false;

        ON.SetActive(false);
        OFF.SetActive(true);

        if (dustParticles != null && dustParticles.isPlaying)
            dustParticles.Stop();
    }

    // -----------------------------
    // 🔋 UI
    // -----------------------------
    void UpdateBatteryUI()
    {
        if (batteryText == null) return;

        int percent = Mathf.RoundToInt(currentBattery);
        batteryText.text = percent + "%";
    }

    // -----------------------------
    // 🔋 FUNKCIJA ZA POLNJENJE
    // To bomo klicali iz BatteryPickup skripte
    // -----------------------------
    public void AddBattery(float amount)
    {
        currentBattery += amount;
        currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery);
    }
}