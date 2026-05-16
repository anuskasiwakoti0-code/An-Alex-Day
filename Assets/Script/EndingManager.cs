using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text endingTitleText;
    public TMP_Text endingDescriptionText;
    public TMP_Text scoreText;
    public TMP_Text mentalHealthText;
    public TMP_Text examConfidenceText;

    [Header("Button References")]
    public Button playAgainButton;
    public Button quitButton;

    private void Start()
    {
        // Unlock cursor for button clicking
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Wire buttons directly in code
        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(OnPlayAgain);
        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuit);

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        int finalScore = GameManager.Instance.GetScore();

        scoreText.text = "Score: " + finalScore + "/4";

        if (mentalHealthText != null)
            mentalHealthText.text = "Mental Health: " +
                GameManager.Instance.GetMentalHealth();

        if (examConfidenceText != null)
            examConfidenceText.text = "Exam Confidence: " +
                GameManager.Instance.GetExamConfidence();

        switch (finalScore)
        {
            case 4:
                ShowEnding("Perfect Day!",
                    "Alex made all the right choices today! " +
                    "Studied hard, handled cyberbullying maturely, " +
                    "helped the family and slept early. " +
                    "Tomorrow's exam will be a breeze!",
                    new Color(0.18f, 0.42f, 0.31f));
                break;
            case 3:
                ShowEnding("A Good Day",
                    "Alex made mostly good choices today. " +
                    "A few things could have been better but " +
                    "overall it was a productive day. " +
                    "The exam should go well!",
                    new Color(0.2f, 0.6f, 0.8f));
                break;
            case 2:
                ShowEnding("Could Be Better",
                    "Alex made some good choices and some poor ones. " +
                    "The balance between responsibilities and " +
                    "distractions needs more work. " +
                    "The exam might be challenging.",
                    new Color(0.9f, 0.7f, 0.1f));
                break;
            case 1:
                ShowEnding("A Difficult Day",
                    "Alex struggled to make good decisions today. " +
                    "Procrastination and poor choices affected " +
                    "studies, relationships and wellbeing. " +
                    "Tomorrow needs to be better.",
                    new Color(0.8f, 0.4f, 0.1f));
                break;
            case 0:
                ShowEnding("Rock Bottom",
                    "Alex made all the wrong choices today. " +
                    "No studying, ignored cyberbullying, " +
                    "refused to help family and stayed up all night. " +
                    "Every day is a chance to start fresh!",
                    new Color(0.76f, 0.07f, 0.12f));
                break;
        }
    }

    private void ShowEnding(string title, string description, Color color)
    {
        endingTitleText.text = title;
        endingTitleText.color = color;
        endingDescriptionText.text = description;
    }

    public void OnPlayAgain()
    {
        Debug.Log("Play Again clicked!");
        GameManager.Instance.ResetScore();
        GameManager.Instance.LoadScene(0);
    }

    public void OnQuit()
    {
        Debug.Log("Quit clicked!");
        GameManager.Instance.QuitGame();
    }
}