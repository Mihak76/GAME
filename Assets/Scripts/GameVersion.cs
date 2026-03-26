using TMPro;
using UnityEngine;

public class GameVersion : MonoBehaviour
{
    public TMP_Text versionText;

    void Start()
    {
        versionText.text = "Version " + Application.version;
    }
}