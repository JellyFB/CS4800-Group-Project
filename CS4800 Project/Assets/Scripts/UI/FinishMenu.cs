using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statsText;

    // On-enable behavior 
    private void OnEnable()
    {
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

        // Update the text to reflect the statistcs of the level
        _statsText.text = $"<b>Completion Time</b>: {GameManager.instance.GetLevelTime(): 0.00} seconds\n" +
            $"<b>Tasks Completed</b>: {TaskManager.instance.TaskCount()}";
    }

    // On-press behavior to continue level 5 
    public void ContinueLevel5Pt1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level5Scene_Pt2");
    }

    public void ContinueLevel5Pt2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level5Scene");
    }


    // On-press behavior for returning back to main menu
    public void BackButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
