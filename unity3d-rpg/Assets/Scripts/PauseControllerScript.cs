using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControllerScript : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    public void OnPauseButtonClicked()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnResumeButtonClicked()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
