using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject skipTextUI;
    public GameObject outroPanel;
    public CanvasGroup fadeScreen;
    public float fadeSpeed = 2f;

    [TextArea(3, 10)]
    public string[] outroParts;

    public float typingSpeed = 0.04f;

    private bool skipRequested = false;

    void Start()
    {
        skipTextUI.SetActive(false);
        StartCoroutine(PlayOutro());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            skipRequested = true;
    }

    IEnumerator PlayOutro()
    {
        foreach (string part in outroParts)
        {
            yield return StartCoroutine(TypeWithGlitch(part));
            yield return new WaitForSeconds(1.2f);
            if (skipRequested) break;
        }

        skipTextUI.SetActive(true);

        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;

        yield return StartCoroutine(FadeAndLoad());
    }

    IEnumerator TypeWithGlitch(string text)
    {
        textComponent.text = "";
        int i = 0;

        while (i < text.Length)
        {
            if (skipRequested)
            {
                textComponent.text = text;
                yield break;
            }

            char letter = text[i];
            textComponent.text += letter;

            if (Random.value < 0.08f && char.IsLetter(letter))
            {
                yield return new WaitForSeconds(typingSpeed * 2f);
                textComponent.text = textComponent.text.Substring(0, textComponent.text.Length - 1);
                yield return new WaitForSeconds(typingSpeed * 2f);
                textComponent.text += letter;
            }

            float delay = typingSpeed;
            if (letter == '.') delay = typingSpeed * 5f;
            else if (letter == ',') delay = typingSpeed * 3f;
            else if (letter == '…') delay = typingSpeed * 7f;

            yield return new WaitForSeconds(delay);
            i++;
        }
    }

    IEnumerator FadeAndLoad()
    {
        skipTextUI.SetActive(false);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            fadeScreen.alpha = t;
            yield return null;
        }

        SceneManager.LoadScene("MainMenu");
    }
}