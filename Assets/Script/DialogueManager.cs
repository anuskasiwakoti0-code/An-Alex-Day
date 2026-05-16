using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_Text dialogueText;

    [Header("Dialogue Messages")]
    public string message1 = "You have an exam tomorrow. Go to your desk...";
    public string message2 = "You need help. Try calling your friend...";
    public string message3 = "That was tough. Your family is nearby...";
    public string message4 = "It's getting late. Time to rest...";

    private int currentMessage = 0;

    private void Start()
    {
        if (dialogueText == null)
        {
            Debug.LogError("DialogueManager: dialogueText is not assigned!");
            return;
        }

        ShowMessage(0);
        Debug.Log("DialogueManager: showing message 1");
    }

    public void ShowNextMessage()
    {
        currentMessage++;
        Debug.Log("DialogueManager: showing message " + (currentMessage + 1));

        if (currentMessage < 4)
            ShowMessage(currentMessage);
        else
            dialogueText.text = "What a day! Head to bed now...";
    }

    private void ShowMessage(int index)
    {
        switch (index)
        {
            case 0: dialogueText.text = message1; break;
            case 1: dialogueText.text = message2; break;
            case 2: dialogueText.text = message3; break;
            case 3: dialogueText.text = message4; break;
        }
    }
}