using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject skipTextUI; // "Press SPACE to skip"

    [TextArea(5,10)]
    public string introText;

    public float typingSpeed = 0.05f;

    private bool isTyping = true;
    private bool skipRequested = false;

    void Start()
    {
        skipTextUI.SetActive(false);
        StartCoroutine(TypeIntro());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipRequested = true;
        }
    }

    IEnumerator TypeIntro()
    {
        textComponent.text = "";

        foreach (char letter in introText)
        {
            if (skipRequested)
            {
                textComponent.text = introText;
                break;
            }

            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        skipTextUI.SetActive(true);

        yield return new WaitForSeconds(1f);

        // počakaj na SPACE ali avtomatski continue
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        EndIntro();
    }

    void EndIntro()
    {
        gameObject.SetActive(false);
        skipTextUI.SetActive(false);

        // TU kasneje daš spawn logic ali loading v game
        Debug.Log("Intro finished → start game here");
    }
}