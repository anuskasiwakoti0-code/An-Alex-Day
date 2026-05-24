using UnityEngine;

public class ChoiceTrigger : MonoBehaviour
{
    [Header("Choice Settings")]
    public string choiceQuestion = "What will you do?";
    public string optionA = "Option A";
    public string optionB = "Option B";
    public bool isGoodChoiceA = true;

    [Header("Choice Tracking")]
    public ChoiceType choiceType = ChoiceType.None;

    public enum ChoiceType
    {
        None,
        StudyChoice,
        CyberbullyingChoice,
        FamilyChoice,
        SleepChoice
    }

    private bool hasTriggered = false;
    private ChoiceManager choiceManager;

    private void OnEnable()
    {
        // Find ChoiceManager every time trigger is enabled
        choiceManager = FindFirstObjectByType<ChoiceManager>();

        if (choiceManager == null)
            Debug.LogWarning("ChoiceManager not found!");
        else
            Debug.Log(gameObject.name + " trigger enabled and ready!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            // Check if game has started via DialogueManager
            DialogueManager dm = FindFirstObjectByType<DialogueManager>();
            if (dm != null && !dm.gameStarted) return;

            // Find ChoiceManager if null
            if (choiceManager == null)
                choiceManager = FindFirstObjectByType<ChoiceManager>();

            if (choiceManager == null)
            {
                Debug.LogError("ChoiceManager is null!");
                return;
            }

            hasTriggered = true;
            choiceManager.ShowChoicePanel(
                choiceQuestion,
                optionA,
                optionB,
                isGoodChoiceA,
                choiceType
            );
        }
    }

    public void ResetTrigger()
    {
        hasTriggered = false;
    }
}