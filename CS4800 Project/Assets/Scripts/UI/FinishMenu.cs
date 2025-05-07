using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    // On-enable behavior 
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
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
