using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    //public string nextScaneName = "Sahne1_YuvayaDoðru";

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;

    }
    void OnVideoEnd (VideoPlayer vp)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
