using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool isPaused;

    [Header("Pause Menu Elements")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _bindsMenu;

    
    void Start()
    {
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _bindsMenu.SetActive(false);
        ResumeGame();
    }

    void Update()
    {
        if(UserInput.instance.MenuOpenCloseInput) {
		    if (isPaused) {
			    ResumeGame();
		    }
		    else {
			    PauseGame();
		    }
	    }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	    Time.timeScale = 0f;
	    isPaused = true;
        GameManager.instance.PauseGameTime(true);

        // Brings up the pause menu
        _pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	    Time.timeScale = 1f;
	    isPaused = false;
        GameManager.instance.PauseGameTime(false);

        // Makes the pause menu elements inactive
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _bindsMenu.SetActive(false);
    }

    public void QuitGame()
    {
		SceneManager.LoadScene("MainMenu");
    }
}

