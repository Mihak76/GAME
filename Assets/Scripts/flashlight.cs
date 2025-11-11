using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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

    private bool isON = false;

    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        if (dustParticles != null)
            dustParticles.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isON = !isON;

            ON.SetActive(isON);
            OFF.SetActive(!isON);

            // 🔊 tu se predvaja zvok
            if (audioSource != null && clickSound != null)
            {
                audioSource.PlayOneShot(clickSound);
            }

            if (dustParticles != null)
            {
                if (isON && !dustParticles.isPlaying)
                    dustParticles.Play();
                else if (!isON && dustParticles.isPlaying)
                    dustParticles.Stop();
            }
        }
    }
}

