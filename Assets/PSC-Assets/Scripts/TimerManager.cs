using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float remainingTime = 80f; // 1분 20초 = 80초
    [SerializeField] private TextMeshProUGUI timerText; // UI 연결용
    [SerializeField] private Image WinUI;
    [SerializeField] private Image LoseUI;
    [SerializeField] private Image DrawUI;
    [SerializeField] private RectTransform LeftPos;
    [SerializeField] private RectTransform RightPos;

    private bool isTimerEnded = false;
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if(!isTimerEnded)
        {
            remainingTime = 0;
            isTimerEnded = true;
            GameManager.instance.isGameOver = true;

            int whoIsWinner = GameManager.instance.GetWinnerID();

            if (whoIsWinner == 1)
            {
                setUI(WinUI, LeftPos);
                setUI(LoseUI, RightPos);
            }
            else if (whoIsWinner == 2)
            {
                setUI(WinUI, RightPos);
                setUI(LoseUI, LeftPos);
            }
            else
            {
                DrawUI.gameObject.SetActive(true);
            }
        }

        timer(remainingTime);
    }

    private void timer(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void setUI(UnityEngine.UI.Image imageUI, RectTransform target)
    {
        if (imageUI != null && target != null)
        {
            imageUI.transform.position = target.position;
            imageUI.gameObject.SetActive(true);
        }
    }
}
