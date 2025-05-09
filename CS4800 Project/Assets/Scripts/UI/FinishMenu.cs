using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class FinishMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statsText;

    private UserHandler _userhandler;

    // On-enable behavior 
    private void OnEnable()
    {
        // Create new userHandler
        _userhandler = new UserHandler();

        // Pause game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;

        // Disable footsteps
        CharacterController player = FindFirstObjectByType<CharacterController>();
        if (player != null)
        {
            player.DisableFootsteps();
        }

        // Update the text to reflect the statistics of the level
        _statsText.text = $"<b>Completion Time</b>: {GameManager.instance.GetLevelTime(): 0.00} seconds\n" +
            $"<b>Tasks Completed</b>: {TaskManager.instance.TaskCount()}";
    }

    // On-press behavior for returning back to main menu
    public void BackButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveData()
    {
        _userhandler.WriteUserData();
    }
}
