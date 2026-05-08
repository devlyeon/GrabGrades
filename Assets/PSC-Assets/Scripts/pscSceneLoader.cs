using UnityEngine;
using UnityEngine.SceneManagement;

public class pscSceneLoader : MonoBehaviour
{
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMainMenu();
            }
        }
    }

    public void RestartGame()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    public void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Splash");
    }
}
