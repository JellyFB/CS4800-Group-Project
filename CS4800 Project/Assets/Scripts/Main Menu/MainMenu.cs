using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentUserText;
    private void OnEnable()
    {
        currentUserText.text = $"Current User: <b>{CurrentUser.s_username}</b>";
    }

    public void Logout()
    {
        CurrentUser.s_username = null;
    }

    public void QuitGame()
    {
		Application.Quit();
    }
}