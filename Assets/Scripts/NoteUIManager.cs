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
        Instance = this;
        notePanel.SetActive(false); // panel je vedno skrit ob startu
    }

    void Update()
    {
        if (noteOpen && Input.GetKeyDown(KeyCode.E))
        {
            CloseNote();
        }
    }

    public void ShowNote(string text)
    {
        notePanel.SetActive(true);
        noteTextUI.text = text;
        noteOpen = true;
        Time.timeScale = 0f;
    }

    void CloseNote()
    {
        notePanel.SetActive(false);
        noteOpen = false;
        Time.timeScale = 1f;
    }
}