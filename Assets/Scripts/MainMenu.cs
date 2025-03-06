using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsMenu; // Assign in the Inspector

    void Start()
    {
        // Ensure SettingsMenu is disabled at start
        if (SettingsMenu != null)
            SettingsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main"); // Load main game scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit application
    }

    public void GoToSettingsMenu()
    {
        if (SettingsMenu != null)
            SettingsMenu.SetActive(true); // Show settings menu
    }

    public void GoToMainMenu()
    {
        if (SettingsMenu != null)
            SettingsMenu.SetActive(false); // Hide settings menu
    }
}

