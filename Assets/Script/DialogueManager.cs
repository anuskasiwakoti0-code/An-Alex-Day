using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_Text dialogueText;

    [Header("UI Panels")]
    public GameObject dialoguePanel;
    public GameObject interactionPrompt;

    [Header("Dialogue Messages")]
    public string message1 = "You have an exam tomorrow. Go to your desk...";
    public string message2 = "You need help. Try calling your friend...";
    public string message3 = "That was tough. Your family is nearby...";
    public string message4 = "It's getting late. Time to rest...";
    public string message5 = "What a day! Head to bed now...";

    private int currentMessage = 0;
    public bool gameStarted = false;

    private void Start()
    {
        if (dialogueText == null)
        {
            Debug.LogError("DialogueManager: dialogueText is not assigned!");
            return;
        }

        // Hide dialogue panel at start
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Show interaction prompt at start
        if (interactionPrompt != null)
            interactionPrompt.SetActive(true);

        Debug.Log("DialogueManager: waiting for E key...");
    }

    private void Update()
    {
        // Wait for E key to start game
        if (!gameStarted && Input.GetKeyDown(KeyCode.E))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        gameStarted = true;

        // Hide interaction prompt
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);

        // Show dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        // Show first message
        ShowMessage(0);
        Debug.Log("DialogueManager: showing message 1");

        // Notify GameFlow to enable first trigger
        FindFirstObjectByType<GameFlow>()?.StartGame();
    }

    public void ShowNextMessage()
    {
        if (!gameStarted) return;

        currentMessage++;

        if (currentMessage <= 4)
        {
            ShowMessage(currentMessage);
            Debug.Log("DialogueManager: showing message " + (currentMessage + 1));
        }
    }

    private void ShowMessage(int index)
    {
        switch (index)
        {
            case 0: dialogueText.text = message1; break;
            case 1: dialogueText.text = message2; break;
            case 2: dialogueText.text = message3; break;
            case 3: dialogueText.text = message4; break;
            case 4: dialogueText.text = message5; break;
        }
    }
}