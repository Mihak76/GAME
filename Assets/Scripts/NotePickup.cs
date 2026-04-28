using UnityEngine;

public class NotePickup : MonoBehaviour
{
    private bool playerInRange = false;

    public string noteText;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectNote();
        }
    }

    void CollectNote()
    {
        NoteManager.Instance.AddNote(noteText);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}