using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void PlayLevel(int level)
    {
        if (level == 1)
            SceneManager.LoadScene("LevelScene");
    }
}
