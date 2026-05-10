using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;

    public int totalNotes = 10;
    public int collectedNotes = 0;

    public List<string> collectedNoteTexts = new List<string>();

    public GameObject outroPanel;
    public TextMeshProUGUI counterText;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void AddNote(string noteText)
    {
        collectedNoteTexts.Add(noteText);
        collectedNotes++;

        UpdateUI();

        if (collectedNotes >= totalNotes)
            AllNotesCollected();
    }

    void UpdateUI()
    {
        if (counterText != null)
            counterText.text = collectedNotes + " / " + totalNotes;
    }

  void AllNotesCollected()
{
    // outro se zdaj sprozil iz NoteUIManager ko zapres zadnjo noto
}
}