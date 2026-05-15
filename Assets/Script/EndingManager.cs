using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text endingTitleText;
    public TMP_Text endingDescriptionText;
    public TMP_Text scoreText;

    [Header("Ending Settings")]
    public int goodEndingMinScore = 2;

    [Header("Good Ending")]
    public string goodEndingTitle = "A Brighter Tomorrow";
    public string goodEndingDescription =
        "Alex made thoughtful choices today. " +
        "By standing up to cyberbullying, helping at home, " +
        "and managing time wisely, Alex is growing into " +
        "a responsible and caring person.";

    [Header("Bad Ending")]
    public string badEndingTitle = "Room To Grow";
    public string badEndingDescription =
        "Today was tough for Alex. " +
        "Some choices led to consequences that affected " +
        "relationships and wellbeing. But every day is " +
        "a new chance to do better.";

    private void Start()
    {
        int finalScore = GameManager.Instance.GetScore();

        scoreText.text = "Your Score: " + finalScore + "/3";

        if (finalScore >= goodEndingMinScore)
            ShowGoodEnding();
        else
            ShowBadEnding();
    }

    private void ShowGoodEnding()
    {
        endingTitleText.text = goodEndingTitle;
        endingTitleText.color = new Color(0.18f, 0.42f, 0.31f);
        endingDescriptionText.text = goodEndingDescription;
    }

    private void ShowBadEnding()
    {
        endingTitleText.text = badEndingTitle;
        endingTitleText.color = new Color(0.76f, 0.07f, 0.12f);
        endingDescriptionText.text = badEndingDescription;
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