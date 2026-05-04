using UnityEngine;

public class SplashButton : MonoBehaviour
{
    public void StartScene(string sceneName)
    {
        SceneLoader.loader.LoadScene(sceneName);
    }

    public void FinishGame()
    {
        Application.Quit();
    }
}