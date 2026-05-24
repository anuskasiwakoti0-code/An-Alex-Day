using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ChoiceManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject choicePanel;
    public TMP_Text questionText;
    public Button optionAButton;
    public Button optionBButton;
    public TMP_Text optionAText;
    public TMP_Text optionBText;
    public GameObject outcomePanel;
    public TMP_Text outcomeText;

    [Header("Settings")]
    public float outcomeDuration = 4f;

    private bool isGoodChoiceA;
    private ChoiceTrigger.ChoiceType currentChoiceType;
    private PlayerInput playerInput;

    private void Start()
    {
        ValidateReferences();
        playerInput = FindFirstObjectByType<PlayerInput>();

        if (choicePanel != null)
            choicePanel.SetActive(false);
        if (outcomePanel != null)
            outcomePanel.SetActive(false);
    }

    private void ValidateReferences()
    {
        if (choicePanel == null)  Debug.LogError("ChoiceManager: choicePanel is not assigned!", this);
        if (questionText == null) Debug.LogError("ChoiceManager: questionText is not assigned!", this);
        if (optionAText == null)  Debug.LogError("ChoiceManager: optionAText is not assigned!", this);
        if (optionBText == null)  Debug.LogError("ChoiceManager: optionBText is not assigned!", this);
        if (outcomePanel == null) Debug.LogError("ChoiceManager: outcomePanel is not assigned!", this);
        if (outcomeText == null)  Debug.LogError("ChoiceManager: outcomeText is not assigned!", this);
    }

    public void ShowChoicePanel(string question,
        string optionA, string optionB, bool goodChoiceA,
        ChoiceTrigger.ChoiceType choiceType)
    {
        if (choicePanel == null || questionText == null ||
            optionAText == null || optionBText == null)
        {
            Debug.LogError("ChoiceManager: Cannot show choice panel — missing references!", this);
            return;
        }

        isGoodChoiceA = goodChoiceA;
        currentChoiceType = choiceType;

        questionText.text = question;
        optionAText.text = optionA;
        optionBText.text = optionB;

        choicePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        if (playerInput != null)
            playerInput.DeactivateInput();
    }

    public void OnChoiceA() => ProcessChoice(true);
    public void OnChoiceB() => ProcessChoice(false);

    private void ProcessChoice(bool choseA)
    {
        if (choicePanel == null || outcomePanel == null || outcomeText == null)
        {
            Debug.LogError("ChoiceManager: Cannot process choice — missing references!", this);
            Time.timeScale = 1f;
            return;
        }

        choicePanel.SetActive(false);
        Time.timeScale = 1f;

        bool isGoodChoice = (choseA == isGoodChoiceA);

        if (isGoodChoice)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddPoint();

                switch (currentChoiceType)
                {
                    case ChoiceTrigger.ChoiceType.StudyChoice:
                        GameManager.Instance.studiedForExam = true;
                        outcomeText.text = "Great choice! Alex sat down and studied hard for the exam. +1 point";
                        break;
                    case ChoiceTrigger.ChoiceType.CyberbullyingChoice:
                        GameManager.Instance.respondedToCyberbullying = true;
                        outcomeText.text = "Well done! Alex blocked and reported the bully calmly. +1 point";
                        break;
                    case ChoiceTrigger.ChoiceType.FamilyChoice:
                        GameManager.Instance.helpedFamily = true;
                        outcomeText.text = "Good job! Alex helped the family with chores willingly. +1 point";
                        break;
                    case ChoiceTrigger.ChoiceType.SleepChoice:
                        GameManager.Instance.sleptEarly = true;
                        outcomeText.text = "Smart! Alex went to bed early to rest well for the exam. +1 point";
                        break;
                    default:
                        outcomeText.text = "Good choice! +1 point";
                        break;
                }
                outcomeText.color = new Color(0.18f, 0.42f, 0.31f);
            }
        }
        else
        {
            switch (currentChoiceType)
            {
                case ChoiceTrigger.ChoiceType.StudyChoice:
                    outcomeText.text = "Alex chose to play games instead of studying. This might affect the exam!";
                    break;
                case ChoiceTrigger.ChoiceType.CyberbullyingChoice:
                    outcomeText.text = "Alex replied with anger making the situation worse.";
                    break;
                case ChoiceTrigger.ChoiceType.FamilyChoice:
                    outcomeText.text = "Alex refused to help and stayed in the room alone.";
                    break;
                case ChoiceTrigger.ChoiceType.SleepChoice:
                    outcomeText.text = "Alex stayed up all night watching movies and playing games!";
                    break;
                default:
                    outcomeText.text = "Not the best choice...";
                    break;
            }
            outcomeText.color = new Color(0.76f, 0.07f, 0.12f);
        }

        outcomePanel.SetActive(true);

        // Update score display
        FindFirstObjectByType<ScoreManager>()?.UpdateDisplay();

        // Notify GameFlow
        FindFirstObjectByType<GameFlow>()?.ChoiceMade();

        // Show next dialogue hint
        FindFirstObjectByType<DialogueManager>()?.ShowNextMessage();

        Invoke(nameof(HideOutcome), outcomeDuration);
    }

    private void HideOutcome()
    {
        if (outcomePanel != null)
            outcomePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerInput != null)
            playerInput.ActivateInput();

        // Advance time of day
        FindFirstObjectByType<DayNightCycle>()?.AdvanceTime(3f);
    }
}