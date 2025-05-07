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

        // Update the text to reflect the statistcs of the level
        _statsText.text = $"<b>Completion Time</b>: {GameManager.instance.GetLevelTime(): 0.00} seconds\n" +
            $"<b>Tasks Completed</b>: {TaskManager.instance.TaskCount()}";
    }

    // On-press behavior for back button
    public void BackButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
