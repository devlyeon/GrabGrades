using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private readonly WaitForSeconds _waitForSeconds0_25 = new(0.25f);
    public static SceneLoader loader;

    [SerializeField] private Animator animator;
    [SerializeField] private Image loadingBar;

    void Awake()
    {
        if (loader != null)
        {
            Destroy(gameObject);
            return;
        }
        
        loader = this;
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName) {
        loadingBar.fillAmount = 0;
        yield return _waitForSeconds0_25;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        float timer = 0.0f;

        while (!async.isDone) {
            timer += Time.deltaTime;
            if (async.progress < 0.9f)
			{
				loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, async.progress, timer);
				if (loadingBar.fillAmount >= async.progress) timer = 0f;
			}
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timer);
                if (loadingBar.fillAmount >= 0.999f)
                {
                    async.allowSceneActivation = true;
                    animator.SetTrigger("Finished"); 
                    yield return _waitForSeconds0_25;
                    gameObject.SetActive(false);
                    yield break;
                }
            }

            yield return null;
        }
    }
}