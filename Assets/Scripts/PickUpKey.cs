using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    [Header("Objects")]
    public GameObject keyOB;       // 3D key v sceni (kar se pobere)
    public GameObject invOB;       // ikona v inventoryju / UI
    public GameObject pickUpText;  // "Press E" text

    [Header("Audio")]
    public AudioSource keySound;

    private bool inReach = false;

    void Start()
    {
        inReach = false;

        if (pickUpText != null) pickUpText.SetActive(false);
        if (invOB != null) invOB.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;
            if (pickUpText != null) pickUpText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            if (pickUpText != null) pickUpText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            if (keyOB != null) keyOB.SetActive(false);

            if (keySound != null) keySound.Play();

            if (invOB != null) invOB.SetActive(true);

            if (pickUpText != null) pickUpText.SetActive(false);

            // opcijsko: da ne moreš še enkrat pobirat
            inReach = false;
        }
    }
}