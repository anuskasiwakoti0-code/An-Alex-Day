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
            Invoke("LoadEnding", 3f);
        }
    }

    private void LoadEnding()
    {
        GameManager.Instance.LoadSceneByName("EndingScene");
    }
}