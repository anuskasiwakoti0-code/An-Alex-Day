using UnityEngine;

public class GameFlow : MonoBehaviour
{
    private int totalChoices = 0;
    public int maxChoices = 4;

    public void ChoiceMade()
    {
        totalChoices++;
        Debug.Log("Choices made: " + totalChoices);

        if (totalChoices >= maxChoices)
        {
            Debug.Log("All choices made — new dawn rising...");
            // Set new dawn lighting first
            FindFirstObjectByType<DayNightCycle>()?.SetNewDawn();
            // Then load ending after short delay
            Invoke("LoadEnding", 4f);
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