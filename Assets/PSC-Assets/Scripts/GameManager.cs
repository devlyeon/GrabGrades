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
    [SerializeField] private float simultaneousHitWindow = 0.05f;

    private GameObject currentItem;
    private int currentItemIndex;
    private int lScore = 0;
    private int rScore = 0;

    private bool lHitCurrent = false;
    private bool rHitCurrent = false;
    
    private readonly int[] gradeScores = { 3, 1, -2 };

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SpawnGrade();
    }

    private void SpawnGrade()
    {
        // 상태 초기화
        lHitCurrent = false;
        rHitCurrent = false;

        if (currentItem != null) Destroy(currentItem);

        currentItemIndex = GetRandomGradeIndex();
        currentItem = Instantiate(gradePrefabs[currentItemIndex], Vector2.zero, Quaternion.identity);
    }

    private int GetRandomGradeIndex()
    {
        int rand = Random.Range(0, 100);

        if (rand < 65) return 1; // B 65%
        if (rand < 80) return 0; // A 15%
        return 2;                // F 20%
    }

    public void ProcessScore(bool isLeftPlayer)
    {
        // 성적표가 없거나 해당 플레이어가 이미 타격했다면 무시
        if (currentItem == null) return;
        if (isLeftPlayer && lHitCurrent) return;
        if (!isLeftPlayer && rHitCurrent) return;

        bool isFirstHit = !lHitCurrent && !rHitCurrent;
        if (isLeftPlayer) lHitCurrent = true;
        else rHitCurrent = true;

        UpdateScore(isLeftPlayer);

        // 처음 닿았을 때 딜레이 주기
        if (isFirstHit)
        {
            StartCoroutine(FinalizeHitWithDelay());
        }
    }

    private void UpdateScore(bool isLeftPlayer)
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
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(delay);
        SpawnGrade();
    }
}
