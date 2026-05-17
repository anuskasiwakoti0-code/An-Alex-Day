using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text endingTitleText;
    public TMP_Text endingDescriptionText;
    public TMP_Text futureDescriptionText;
    public TMP_Text scoreText;
    public TMP_Text mentalHealthText;
    public TMP_Text examConfidenceText;
    public RawImage backgroundImage;

    [Header("Background Images")]
    public Texture2D perfectEndingBg;
    public Texture2D goodEndingBg;
    public Texture2D averageEndingBg;
    public Texture2D difficultEndingBg;
    public Texture2D rockBottomEndingBg;

    private void Start()
    {
        // Unlock cursor for button clicking
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Restore time if paused
        Time.timeScale = 1f;

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
                ShowEnding(
                    "Perfect Day! A New Dawn",
                    "Alex made all the right choices today!",
                    "If you keep making these choices, you will pass " +
                    "your exam with flying colors, build strong " +
                    "relationships and grow into a confident young adult.",
                    new Color(1f, 0.85f, 0f),
                    perfectEndingBg
                );
                break;

            case 3:
                ShowEnding(
                    "A Good Day",
                    "Alex made mostly good choices today.",
                    "If you keep making mostly good choices, you are " +
                    "on the right path. A few improvements will " +
                    "take you far in life.",
                    new Color(0.31f, 0.76f, 0.97f),
                    goodEndingBg
                );
                break;

            case 2:
                ShowEnding(
                    "Could Be Better",
                    "Alex made some good and some poor choices.",
                    "Your mixed choices show potential but " +
                    "inconsistency. Focus on your priorities " +
                    "and you will see great improvement.",
                    new Color(1f, 0.95f, 0.46f),
                    averageEndingBg
                );
                break;

            case 1:
                ShowEnding(
                    "A Difficult Day",
                    "Alex struggled to make good decisions today.",
                    "Your poor choices are affecting your future. " +
                    "It is not too late to change. Start making " +
                    "better decisions today.",
                    new Color(1f, 0.54f, 0.4f),
                    difficultEndingBg
                );
                break;

            case 0:
                ShowEnding(
                    "Rock Bottom",
                    "Alex made all the wrong choices today.",
                    "Every wrong choice has a consequence. " +
                    "But every new day is a chance to start fresh " +
                    "and choose differently.",
                    new Color(0.94f, 0.33f, 0.31f),
                    rockBottomEndingBg
                );
                break;
        }
    }

    private void ShowEnding(string title, string description,
        string future, Color titleColor, Texture2D bgImage)
    {
        endingTitleText.text = title;
        endingTitleText.color = titleColor;
        endingDescriptionText.text = description;

        if (futureDescriptionText != null)
            futureDescriptionText.text = future;

        if (backgroundImage != null && bgImage != null)
        {
            backgroundImage.texture = bgImage;
            Debug.Log("Background image set: " + bgImage.name);
        }
        else
        {
            Debug.LogWarning("Background image not set! backgroundImage: " +
                backgroundImage + " bgImage: " + bgImage);
        }
    }

    public void OnPlayAgain()
    {
        GameManager.Instance.ResetScore();
        GameManager.Instance.LoadSceneByName("TitleScreen");
    }

    public void OnQuit()
    {
        GameManager.Instance.QuitGame();
    }
}