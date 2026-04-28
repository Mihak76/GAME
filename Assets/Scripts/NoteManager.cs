using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;

    public int totalNotes = 10;
    public int collectedNotes = 0;

    public List<string> collectedNoteTexts = new List<string>();

    public TextMeshProUGUI counterText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddNote(string noteText)
    {
        collectedNoteTexts.Add(noteText);
        collectedNotes++;

        UpdateUI();

        if (collectedNotes >= totalNotes)
        {
            AllNotesCollected();
        }
    }

    void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = collectedNotes + " / " + totalNotes;
        }
    }

    void AllNotesCollected()
    {
        Debug.Log("ALL NOTES COLLECTED → END GAME TRIGGER");
    }
}