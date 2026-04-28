using System.Collections;
using TMPro;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject skipTextUI;
    public GameObject introPanel;

    public CanvasGroup fadeScreen;
    public float fadeSpeed = 2f;

    [TextArea(3, 10)]
    public string[] introParts;

    public float typingSpeed = 0.04f;

    private bool skipRequested = false;

    void Start()
    {
        skipTextUI.SetActive(false);
        StartCoroutine(PlayIntro());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipRequested = true;
        }
    }

    IEnumerator PlayIntro()
    {
        foreach (string part in introParts)
        {
            yield return StartCoroutine(TypeText(part));

            yield return new WaitForSeconds(1.2f);

            if (skipRequested)
                break;
        }

        skipTextUI.SetActive(true);

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        yield return StartCoroutine(EndIntro());
    }

    IEnumerator TypeText(string text)
    {
        textComponent.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (skipRequested)
            {
                textComponent.text = text;
                yield break;
            }

            textComponent.text += text[i];

            float delay = typingSpeed;

            if (text[i] == '.')
                delay = typingSpeed * 5f;
            else if (text[i] == ',')
                delay = typingSpeed * 3f;
            else if (text[i] == '…')
                delay = typingSpeed * 7f;

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator EndIntro()
    {
        skipTextUI.SetActive(false);

        // 🔥 FADE TO BLACK
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            fadeScreen.alpha = t;
            yield return null;
        }

        // 🔥 TU GREŠ V GAME (spawn, scene load, itd.)
        introPanel.SetActive(false);

        Debug.Log("Game start here (spawn player / campfire)");
    }
}