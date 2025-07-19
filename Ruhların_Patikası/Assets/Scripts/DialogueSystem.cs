using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class DialogueSystem : MonoBehaviour
{
    [Header("UI Elemanları")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Diyalog Satırları")]
    [TextArea(2, 5)]
    public string[] lines;

    [Header("Yazı Ayarı")]
    public float typingSpeed = 0.02f;

    private int currentLine = 0;
    private bool isTyping = false;
    private bool dialogueActive = false; 

    void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            currentLine++;
            if (currentLine < lines.Length)
            {
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void StartDialogue()
    {
        currentLine = 0;
        dialoguePanel.SetActive(true);
        dialogueActive = true;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in lines[currentLine])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueActive = false;

        // Sahne geçişi
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
