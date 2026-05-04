using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameObject[] grades;
    public TextMeshProUGUI lscoreText;
    public TextMeshProUGUI rscoreText;

    private GameObject currentItem;
    private int itemIndex;
    private int lscore = 0;
    private int rscore = 0;

    // 현재 판정 대기 중인 입력이 왼쪽인지 오른쪽인지 저장
    private bool lastInputWasLeft;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        getRandomGrades();
    }

    void Update()
    {
        // 1. Update에서는 애니메이션 실행 신호만 보냅니다.
        // 점수 계산(getScore)은 여기서 호출하지 않습니다.
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInputWasLeft = true;
            // 여기에 왼쪽 팔 Animator 트리거 실행 코드 추가
            // leftArmAnimator.SetTrigger("A_Press");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            lastInputWasLeft = false;
            // 여기에 오른쪽 팔 Animator 트리거 실행 코드 추가
            // rightArmAnimator.SetTrigger("L_Press");
        }
    }

    void getRandomGrades()
    {
        if(currentItem != null) Destroy(currentItem);
        
        itemIndex = Random.Range(0, grades.Length);
        currentItem = Instantiate(grades[itemIndex], Vector2.zero, Quaternion.identity);
    }

    private bool isProcessing = false; // 중복 실행 방지 플래그

    public void ProcessScore(bool isLP)
    {
        if (isProcessing) return; // 이미 처리 중이면 무시
        isProcessing = true;

        int score = 0;
        switch (itemIndex)
        {
            case 0: score = 1; break;
            case 1: score = -1; break;
            default: break;
        }

        if (isLP)
        {
            lscore += score;
            lscoreText.text = lscore.ToString();
        }
        else
        {
            rscore += score;
            rscoreText.text = rscore.ToString();
        }
    
        getRandomGrades();

        // 다음 아이템이 생성된 후 아주 짧은 대기 후 다시 허용
        Invoke("ResetProcessing", 0.1f); 
    }

    void ResetProcessing()
    {
        isProcessing = false;
    }
}