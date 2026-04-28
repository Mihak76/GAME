using UnityEngine;
using TMPro;

public class NoteUIManager : MonoBehaviour
{
    public static NoteUIManager Instance;

    public GameObject notePanel;
    public TextMeshProUGUI noteTextUI;

    private bool noteOpen = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (notePanel != null)
            notePanel.SetActive(false);
    }

    void Update()
    {
        if (!noteOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            CloseNote();
        }
    }

    public void ShowNote(string text)
    {
        notePanel.SetActive(true);
        noteTextUI.text = text;
        noteOpen = true;

        // IMPORTANT: reset input buffer (fix "stuck E" bug)
        Input.ResetInputAxes();
    }

    void CloseNote()
    {
        notePanel.SetActive(false);
        noteOpen = false;

        Input.ResetInputAxes();
    }

    public bool IsOpen()
    {
        return noteOpen;
    }
}