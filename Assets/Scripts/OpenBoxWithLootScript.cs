using UnityEngine;

public class OpenBoxWithLootScript : MonoBehaviour
{
    public Animator boxOB;
    public GameObject keyOBNeeded;
    public GameObject openText;
    public GameObject keyMissingText;
    public AudioSource openSound;

    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public GameObject drop4;
    public GameObject drop5;
    public GameObject drop6;

    private bool inReach = false;
    private bool isOpen = false;
    private int randomNumber;

    void Start()
    {
        randomNumber = Random.Range(0, 6); // 0–5 (da lahko pade tudi drop6)

        if (openText != null) openText.SetActive(false);
        if (keyMissingText != null) keyMissingText.SetActive(false);
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
            if (keyOBNeeded != null && keyOBNeeded.activeInHierarchy)
            {
                keyOBNeeded.SetActive(false);

                if (openSound != null) openSound.Play();
                if (boxOB != null) boxOB.SetBool("open", true);

                if (openText != null) openText.SetActive(false);
                if (keyMissingText != null) keyMissingText.SetActive(false);

                SpawnLoot();
                isOpen = true;

                // izklopi collider, da ne moreš še enkrat
                Collider col = GetComponent<Collider>();
                if (col != null) col.enabled = false;
            }
            else
            {
                if (openText != null) openText.SetActive(false);
                if (keyMissingText != null) keyMissingText.SetActive(true);
            }
        }
    }

    void SpawnLoot()
    {
        if (randomNumber == 0 && drop1 != null) drop1.SetActive(true);
        else if (randomNumber == 1 && drop2 != null) drop2.SetActive(true);
        else if (randomNumber == 2 && drop3 != null) drop3.SetActive(true);
        else if (randomNumber == 3 && drop4 != null) drop4.SetActive(true);
        else if (randomNumber == 4 && drop5 != null) drop5.SetActive(true);
        else if (randomNumber == 5 && drop6 != null) drop6.SetActive(true);
    }
}