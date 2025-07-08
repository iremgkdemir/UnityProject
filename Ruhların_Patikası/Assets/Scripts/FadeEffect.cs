using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    public void StartFadeOut(string nextSceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(nextSceneName));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        Color color = fadeImage.color;
        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, 1f);
        SceneManager.LoadScene(sceneName);
    }
}
