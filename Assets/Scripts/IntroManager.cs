using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public AudioSource narrationAudio;

    [System.Serializable]
    public class SubtitleLine
    {
        public string text;
        public float startTime;
        public float endTime;
    }

    public SubtitleLine[] lines;
    public string nextSceneName = "MainGame";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlayIntro()
    {
        narrationAudio.Play();

        foreach (var line in lines)
        {
            subtitleText.text = "";
            yield return new WaitForSeconds(line.startTime - narrationAudio.time);
            yield return StartCoroutine(FadeIn(line.text, 1f));
            yield return new WaitForSeconds(line.endTime - line.startTime);
            yield return StartCoroutine(FadeOut(1f));
        }

        subtitleText.text = "";
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneName);
    }
    
    IEnumerator FadeIn(string text, float duration)
    {
        subtitleText.text = text;
        Color color = subtitleText.color;
        color.a = 0;
        subtitleText.color = color;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            subtitleText.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut(float duration)
    {
        Color color = subtitleText.color;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsed / duration));
            subtitleText.color = color;
            yield return null;
        }

        subtitleText.text = "";
    }
}
