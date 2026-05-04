using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float remainingTime = 80f; // 1분 20초 = 80초
    public TextMeshProUGUI timerText; // UI 연결용

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            remainingTime = 0;
            // 여기서 게임 오버 처리 등을 할 수 있습니다.
            timerText.color = Color.red; 
        }

        timer(remainingTime);
    }

    void timer(float timeToDisplay)
    {
        // 시간을 분과 초로 계산
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // 00:00 형식으로 텍스트 업데이트
        // {0:00}은 두 자리 숫자를 유지하며, 한 자리일 경우 앞에 0을 채웁니다.
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
