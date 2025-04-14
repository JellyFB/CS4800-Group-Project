using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void PlayLevel(int level)
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("LevelScene");
                break;
            case 2:
                SceneManager.LoadScene("InitialLevelTest");
                break;
            case 3:
                SceneManager.LoadScene("Level3Scene");
                break;
            default:
                break;
        }
            
    }
}
