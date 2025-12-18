using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
    }
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitButtonPressed()
    {
        
    }
}
