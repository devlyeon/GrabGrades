using System.Collections;
using UnityEngine;
using TMPro;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject timerManager;
    [SerializeField] private TextMeshProUGUI startCounter;
    [SerializeField] private int count = 3;

    void Start()
    {
        StartCoroutine(Countdown());
    }
    
    IEnumerator Countdown()
    {
        while (count > 0)
        {
            startCounter.text = count.ToString();
            yield return new WaitForSeconds(1.0f); // 정확히 1초 대기
            count--;
        }
        startCounter.text = "START!";
        startCounter.gameObject.SetActive(false);
        timerManager.SetActive(true);
        //yield return new WaitForSeconds(1.0f); // 정확히 1초 대기
        gameManager.SetActive(true);
    }
    
}
