using UnityEngine;
using TMPro;

public class MessageZone : MonoBehaviour
{
    public string messageToShow = "Hoþ geldin yolcu!";
    public float displayDuration = 3f;

    private GameObject panel;
    private TextMeshProUGUI messageText;

    void Start()
    {
        panel = GameObject.Find("MessagePanel");
        if (panel != null)
            messageText = panel.GetComponentInChildren<TextMeshProUGUI>();

        if (panel != null)
            panel.SetActive(false); // baþta kapalý olsun
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && panel != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowMessage());
        }
    }

    System.Collections.IEnumerator ShowMessage()
    {
        panel.SetActive(true);
        messageText.text = messageToShow;

        yield return new WaitForSeconds(displayDuration);

        panel.SetActive(false);
        messageText.text = "";
    }
}

