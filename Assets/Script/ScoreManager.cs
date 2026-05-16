using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_Text scoreDisplayText;

    [Header("Settings")]
    public string scorePrefix = "Score: ";
    public int maxScore = 3;

    private void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (scoreDisplayText == null) return;

        if (GameManager.Instance != null)
        {
            int current = GameManager.Instance.GetScore();
            scoreDisplayText.text = scorePrefix + current + "/" + maxScore;
        }
    }
}