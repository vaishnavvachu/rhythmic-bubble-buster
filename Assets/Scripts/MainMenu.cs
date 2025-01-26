using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SoundManager soundManager; 

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void PlayGame()
    {
        if (soundManager != null)
        {
            soundManager.PlayButtonClickSound();
        }
        
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        if (soundManager != null)
        {
            soundManager.PlayButtonClickSound();
        }
        Application.Quit();
    }
}
