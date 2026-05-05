using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI lScoreText;
    [SerializeField] private TextMeshProUGUI rScoreText;

    [Header("Game Settings")]
    [SerializeField] private GameObject[] gradePrefabs;
    [SerializeField] private float minSpawnDelay = 2f;
    [SerializeField] private float maxSpawnDelay = 4.5f;
    [SerializeField] private float simultaneousHitWindow = 0.05f; // 동시 득점 가능 시간 - 해당 시간동안 팔이 닿아도 성적표가 사라지지 않음

    private GameObject currentItem;
    private int currentItemIndex;
    private int lScore = 0;
    private int rScore = 0;

    private bool lHitCurrent = false;
    private bool rHitCurrent = false;
    
    private readonly int[] gradeScores = { 3, 1, -2 };

    public bool isGameOver = true;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SpawnGrade();
        isGameOver = false;
        SoundManager.instance.PlayBGM(SoundType.BGM);
    }

    private void SpawnGrade()
    {
        if (!isGameOver)
        {
            // 상태 초기화
            lHitCurrent = false;
            rHitCurrent = false;

            if (currentItem != null) Destroy(currentItem); // 기존의 성적표 제거

            currentItemIndex = GetRandomGradeIndex();
            currentItem =
                Instantiate(gradePrefabs[currentItemIndex], Vector2.zero, Quaternion.identity); // 랜덤으로 선택된 오브젝트 생성
        }
    }

    private int GetRandomGradeIndex() // 각 성적표 생성 확률 조정
    {
        int rand = Random.Range(0, 100);

        if (rand < 65) return 1; // B 65%
        if (rand < 80) return 0; // A 15%
        return 2;                // F 20%
    }

    public void ProcessScore(bool isLeftPlayer)
    {
        // 성적표가 없거나 해당 플레이어가 이미 팔을 뻗은 상태면 무시
        if (currentItem == null) return;
        if (isLeftPlayer && lHitCurrent) return;
        if (!isLeftPlayer && rHitCurrent) return;

        bool isFirstHit = !lHitCurrent && !rHitCurrent;
        if (isLeftPlayer) lHitCurrent = true;
        else rHitCurrent = true;

        UpdateScore(isLeftPlayer);

        // 처음 누군가가 득/실점 했을 때 동시 득점을 위해 약간 대기 및 코루틴 실행
        if (isFirstHit)
        {
            StartCoroutine(FinalizeHitWithDelay());
        }
    }

    private void UpdateScore(bool isLeftPlayer) // 득점 - 왼쪽 플레이거 득점을 한게 아니라면 반드시 오른쪽 플레이어가 득점을 한다고 가정
    {
        int scoreDelta = gradeScores[currentItemIndex];

        if (isLeftPlayer)
        {
            lScore += scoreDelta;
            lScoreText.text = lScore.ToString();
        }
        else
        {
            rScore += scoreDelta;
            rScoreText.text = rScore.ToString();
        }

        if ((currentItemIndex == 0 || currentItemIndex == 1) && currentItem != null)
        {
            SoundManager.instance.PlaySFX(SoundType.GetSFX);
        }
        else if (currentItemIndex == 2 && currentItem != null)
        {
            SoundManager.instance.PlaySFX(SoundType.LostSFX);
        }
    }

    private IEnumerator FinalizeHitWithDelay()
    {
        // 동시 입력 허용 시간 대기
        yield return new WaitForSeconds(simultaneousHitWindow);

        // 현재 성적표 제거 및 대기 상태 진입
        if (currentItem != null)
        {
            Destroy(currentItem);
            currentItem = null;
        }

        StartCoroutine(WaitAndSpawnNext());
    }

    private IEnumerator WaitAndSpawnNext()
    {
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay); // 설정한 시간 값 사이에서 랜덤으로 지연 시간 설정
        yield return new WaitForSeconds(delay);
        SpawnGrade(); // 성적 생성
    }
    
    public int GetWinnerID() // 승자 확인
    {
        if (lScore > rScore) return 1; // 왼쪽 플레이어 승리
        if (rScore > lScore) return 2; // 오른쪽 플레이어 승리
        return 0; // 비겼을 때
    }
}
