using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public float waitAfterFade = 1.5f; // ✨ Sahne geçmeden önce bekleme süresi

    public DialogueSystem dialogueSystem;

    public void StartFadeOut(string nextSceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(nextSceneName));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        Color color = fadeImage.color;
        float time = 0f;

        // Fade işlemi
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        // Tam siyaha geçiş garantisi
        fadeImage.color = new Color(color.r, color.g, color.b, 1f);

        // ✨ Fade bittikten sonra yazının görünmesi için bekle
        yield return new WaitForSeconds(waitAfterFade);

        // Diyalog başlat
        if (dialogueSystem != null)
        {
            Debug.Log("Diyalog başlatılıyor...");
            dialogueSystem.StartDialogue();
        }
        else
        {
            Debug.LogWarning("DialogueSystem atanmadı! (null)");
        }


        // Sahne geçişi
       // SceneManager.LoadScene(sceneName);
    }
}
