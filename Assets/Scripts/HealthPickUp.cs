using System.Collections;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public GameObject pickUpOB;
    public GameObject player;
    public GameObject pickUpText;
    public GameObject cannotPickUpText;
    public float addHealth = 25f;

    public AudioSource healthPickUpSound;
    public GameObject screenFX;

    private bool inReach;

    void Start()
    {
        pickUpText.SetActive(false);
        cannotPickUpText.SetActive(false);
        screenFX.SetActive(false);
        inReach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            pickUpText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            pickUpText.SetActive(false);
            cannotPickUpText.SetActive(false);
        }
    }

    void Update()
    {
        if (!inReach) return;

        PlayerHealth ph = player.GetComponent<PlayerHealth>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ph.health < 100)
            {
                ph.health += addHealth;
                ph.health = Mathf.Clamp(ph.health, 0, 100);

                healthPickUpSound.Play();
                screenFX.SetActive(true);

                pickUpOB.GetComponent<Collider>().enabled = false;
                pickUpOB.GetComponent<MeshRenderer>().enabled = false;
                pickUpText.SetActive(false);

                StartCoroutine(TurnScreenFXOFF());
            }
            else
            {
                pickUpText.SetActive(false);
                cannotPickUpText.SetActive(true);
            }
        }
        
  
    }

    IEnumerator TurnScreenFXOFF()
    {
        yield return new WaitForSeconds(1.25f);
        screenFX.SetActive(false);
        pickUpOB.SetActive(false);
    }
}
