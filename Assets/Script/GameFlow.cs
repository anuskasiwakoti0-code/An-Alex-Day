using UnityEngine;

public class GameFlow : MonoBehaviour
{
    private int totalChoices = 0;
    public int maxChoices = 4;

    [Header("Triggers In Order")]
    public ChoiceTrigger trigger1;
    public ChoiceTrigger trigger2;
    public ChoiceTrigger trigger3;
    public ChoiceTrigger trigger4;

    private ChoiceTrigger[] allTriggers;

    private void Start()
    {
        allTriggers = new ChoiceTrigger[]
        { trigger1, trigger2, trigger3, trigger4 };

        // Disable all trigger colliders at start
        DisableAllTriggers();
    }

    public void StartGame()
    {
        EnableTrigger(0);
        Debug.Log("GameFlow: game started, trigger 1 enabled");
    }

    public void ChoiceMade()
    {
        totalChoices++;
        Debug.Log("Choices made: " + totalChoices);

        DisableAllTriggers();

        if (totalChoices >= maxChoices)
        {
            Debug.Log("All choices made — new dawn rising...");
            FindFirstObjectByType<DayNightCycle>()?.SetNewDawn();
            Invoke("LoadEnding", 4f);
        }
        else
        {
            EnableTrigger(totalChoices);
            Debug.Log("Enabled trigger " + (totalChoices + 1));
        }
    }

    private void EnableTrigger(int index)
    {
        if (allTriggers[index] != null)
        {
            // Enable both collider and script
            Collider col = allTriggers[index].GetComponent<Collider>();
            if (col != null) col.enabled = true;
            allTriggers[index].enabled = true;
            Debug.Log("Enabled: " + allTriggers[index].gameObject.name);
        }
    }

    private void DisableAllTriggers()
    {
        foreach (ChoiceTrigger trigger in allTriggers)
        {
            if (trigger != null)
            {
                // Disable both collider and script
                Collider col = trigger.GetComponent<Collider>();
                if (col != null) col.enabled = false;
                trigger.enabled = false;
            }
        }
    }

    private void LoadEnding()
    {
        Debug.Log("Loading EndingScene...");
        if (GameManager.Instance != null)
            GameManager.Instance.LoadSceneByName("EndingScene");
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}