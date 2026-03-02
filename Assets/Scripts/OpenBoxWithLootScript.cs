using UnityEngine;

public class OpenBoxWithLootScript : MonoBehaviour
{
    [Header("Box")]
    public Animator boxOB;
    public AudioSource openSound;

    [Header("Key")]
    public GameObject keyOBNeeded;

    [Header("UI")]
    public GameObject openText;
    public GameObject keyMissingText;

    [Header("Drops (iz Hierarchy)")]
    public GameObject[] drops;   // Size = 6 → povleci iteme iz Hierarchy

    private bool inReach = false;
    private bool isOpen = false;

    void Start()
    {
        if (openText != null) openText.SetActive(false);
        if (keyMissingText != null) keyMissingText.SetActive(false);

        // Na začetku ugasni vse drop iteme
        foreach (GameObject d in drops)
            if (d != null)
                d.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            inReach = true;
            if (openText != null) openText.SetActive(true);
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
        if (!inReach || isOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Če nimaš ključa
            if (keyOBNeeded != null && !keyOBNeeded.activeInHierarchy)
            {
                if (openText != null) openText.SetActive(false);
                if (keyMissingText != null) keyMissingText.SetActive(true);
                Debug.Log("[Box] Player nima ključa");
                return;
            }

            // Odpri box
            if (boxOB != null) boxOB.SetBool("open", true);
            if (openSound != null) openSound.Play();

            if (openText != null) openText.SetActive(false);
            if (keyMissingText != null) keyMissingText.SetActive(false);

            SpawnLoot();
            isOpen = true;

            // izklopi trigger
            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }
    }

    void SpawnLoot()
    {
        if (drops == null || drops.Length == 0)
        {
            Debug.LogWarning("[BoxLoot] Drops array je prazen!");
            return;
        }

        int r = Random.Range(0, drops.Length);

        if (drops[r] == null)
        {
            Debug.LogWarning("[BoxLoot] Drop " + r + " je NULL v Inspectorju!");
            return;
        }

        drops[r].SetActive(true);
        Debug.Log("[BoxLoot] Spawned: " + drops[r].name);
    }
}