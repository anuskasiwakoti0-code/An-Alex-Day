using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Score tracking
    private int score = 0;

    // Track specific choices
    public bool studiedForExam = false;
    public bool respondedToCyberbullying = false;
    public bool helpedFamily = false;
    public bool sleptEarly = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddPoint()
    {
        score++;
        Debug.Log("Score: " + score);
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        studiedForExam = false;
        respondedToCyberbullying = false;
        helpedFamily = false;
        sleptEarly = false;
        Debug.Log("Score Reset");
    }

    // Mental Health based on cyberbullying choice
    public string GetMentalHealth()
    {
        if (respondedToCyberbullying)
            return "Good — You handled it maturely";
        else
            return "Poor — Reacting with anger made things worse";
    }

    // Exam Confidence based on study + sleep choices
    public string GetExamConfidence()
    {
        if (studiedForExam && sleptEarly)
            return "Very Confident — Well prepared and rested";
        else if (studiedForExam || sleptEarly)
            return "Fairly Confident — Could have done more";
        else
            return "Not Ready — Neither studied nor slept early";
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadNextScene()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}