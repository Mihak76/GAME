using UnityEngine;

public class OpenBoxScript : MonoBehaviour
{
    [Header("Box")]
    public Animator boxAnimator;          // Animator od škatle (lahko je na istem objektu)
    public Collider boxCollider;          // Collider od škatle (ponavadi BoxCollider na istem objektu)

    [Header("Key / UI")]
    public GameObject keyOBNeeded;        // Ikona ključa v inventoryju (invOB)
    public GameObject openText;           // "Press E to open"
    public GameObject keyMissingText;     // "You need a key"

    [Header("Audio")]
    public AudioSource openSound;

    private bool inReach = false;
    private bool isOpen = false;

    void Start()
    {
        inReach = false;

        if (openText != null) openText.SetActive(false);
        if (keyMissingText != null) keyMissingText.SetActive(false);

        // če nisi nastavil v Inspectorju, poskusi avtomatsko najt
        if (boxAnimator == null) boxAnimator = GetComponent<Animator>();
        if (boxCollider == null) boxCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            inReach = true;
            if (openText != null) openText.SetActive(true);
            if (keyMissingText != null) keyMissingText.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            if (openText != null) openText.SetActive(false);
            if (keyMissingText != null) keyMissingText.SetActive(false);
        }
    }

    void Update()
    {
        if (isOpen) return;

        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            // če imaš key v inventoryju (aktiven inv icon)
            if (keyOBNeeded != null && keyOBNeeded.activeInHierarchy)
            {
                keyOBNeeded.SetActive(false);

                if (openSound != null) openSound.Play();

                if (boxAnimator != null)
                    boxAnimator.SetBool("open", true);

                if (openText != null) openText.SetActive(false);
                if (keyMissingText != null) keyMissingText.SetActive(false);

                isOpen = true;

                // onemogoči collider & skripto, da se ne proži več
                if (boxCollider != null) boxCollider.enabled = false;
                enabled = false;
            }
            else
            {
                // nima ključa
                if (openText != null) openText.SetActive(false);
                if (keyMissingText != null) keyMissingText.SetActive(true);
            }
        }
    }
}