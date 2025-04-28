using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _loginPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Checks if the user has already logged in or not.
        if (GameManager.instance.currentUsername == null || GameManager.instance.currentUsername.Equals(""))
        {
            _loginPanel.SetActive(true);
            _mainMenu.SetActive(false);
        }
        else
        {
            Debug.Log(GameManager.instance.currentUsername);
            _loginPanel.SetActive(false);
            _mainMenu.SetActive(true);
        }
    }
}
