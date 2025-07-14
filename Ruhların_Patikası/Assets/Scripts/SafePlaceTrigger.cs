using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 
using System.Collections;

public class SafePlaceTrigger : MonoBehaviour
{
    public string nextSceneName;
    public Image fadeImage;         
    public TextMeshProUGUI dialogText; 
    public float fadeDuration = 1f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(DialogAndTransition());
        }
    }

    private IEnumerator DialogAndTransition()
    {
        yield return StartCoroutine(Fade(0f, 1f));

        int saved = GameManager.Instance.rescuedAnimals;
        dialogText.text = $"{saved} hayvanı kurtardın.\nSıradaki maceraya geçme vakti...";
        yield return new WaitForSeconds(2.5f);

        dialogText.text = "";

        yield return StartCoroutine(Fade(1f, 0f));

        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = c;
            yield return null;
        }

        c.a = endAlpha;
        fadeImage.color = c;
    }
}
