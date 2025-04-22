using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool isPaused;

    [Header("Pause Menu Elements")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingsMenu;

    
    void Start()
    {
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        ResumeGame();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
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

        // Brings up the pause menu
        _pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	    Time.timeScale = 1f;
	    isPaused = false;

        // Makes the pause menu elements inactive
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
		SceneManager.LoadScene("MainMenu");
    }
}

