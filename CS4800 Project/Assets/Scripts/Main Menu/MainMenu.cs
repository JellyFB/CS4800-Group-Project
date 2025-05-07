using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentUserText;
    
    // Called whenever the object is enabled.
    public void OnEnable()
    {
        // Displays the current user on the top left of the main menu
        currentUserText.text = $"Current User: <b>{GameManager.instance.currentUsername}</b>";
    }

    // On-click behavior of the logout button.
    public void Logout()
    {
        // Logs the player out
        GameManager.instance.currentUsername = null;
    }

    // On-click behavior of the quit button.
    public void QuitGame()
    {
        // Closes the simulator
		Application.Quit();
    }
}