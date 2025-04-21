using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void PlayLevel(int level)
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("Level1Scene");
                break;
            case 2:
                SceneManager.LoadScene("Level2Scene");
                break;
            case 3:
                SceneManager.LoadScene("Level3Scene");
                break;
            case 4:
                SceneManager.LoadScene("Level4Scene");
                break;
            case 5:
                break;
            default:
                break;
        }
            
    }
}
