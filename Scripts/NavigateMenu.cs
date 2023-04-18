using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateMenu : MonoBehaviour
{

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        if (Time.timeScale == 0) Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
