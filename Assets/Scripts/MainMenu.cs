using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public void GoToSettingsMenu()
    {
        // Open the options menu
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToMainMenu()
    {
        // Open the about menu
        SceneManager.LoadScene("MainMenu");
    }
}
