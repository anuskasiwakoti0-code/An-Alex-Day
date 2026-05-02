using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    private void OnStartClicked()
    {
        // Scene 1 = StudyRoom
        GameManager.Instance.LoadScene(1);
    }

    private void OnQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}