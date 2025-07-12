using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Appelé par ton bouton Play
    public void PlayGame()
    {
        // Attention au nom exact de ta scène :
        SceneManager.LoadScene("GamePlay");
    }

    // Appelé par ton bouton Quit
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

