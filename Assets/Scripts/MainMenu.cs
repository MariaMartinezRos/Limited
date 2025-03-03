using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingsMenu; // Assign in the Inspector

    void Start()
    {
        //the options menu is disabled
        if (SettingsMenu != null)
            SettingsMenu.SetActive(false);
    }
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
        if (SettingsMenu != null)
            SettingsMenu.SetActive(true); // Show settings menu

    }

    public void GoToMainMenu()
    {
        // Open the about menu
        if (SettingsMenu != null)
            SettingsMenu.SetActive(false); // Hide settings menu
        SceneManager.LoadScene("MainMenu");
    }
}
