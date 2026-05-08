using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

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
    
    private Dictionary<KeyCode, float> keyHeat = new Dictionary<KeyCode, float>(); // 키 코드 별로 연타 감지
    
    private GameObject currentItem;
    private int currentItemIndex;
    private int lScore = 0;
    private int rScore = 0;

    private int defaultB = 65; // B나올 기본 확률
    private int defaultA = 15; // A나올 기본 확률
    
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
        //SoundManager.instance.PlayBGM(SoundType.BGM);
    }

    void Update()
    {
        if (isGameOver) return;

        bool lPenalty = CheckPenalty(KeyCode.A);
        bool rPenalty = CheckPenalty(KeyCode.L);

        if (lPenalty || rPenalty)
        {
            ApplyPenalty(); 
            Debug.Log("페널티 적용");
            if (lPenalty) keyHeat[KeyCode.A] = 0f;
            if (rPenalty) keyHeat[KeyCode.L] = 0f;
        }
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

        if (rand < defaultB) return 1; // B 65%
        if (rand < defaultB + defaultA) return 0; // A 15%
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

    private void UpdateScore(bool isLeftPlayer) // 득점 - 왼쪽 플레이어가 득점을 한게 아니라면 반드시 오른쪽 플레이어가 득점을 한다고 가정
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

        if (currentItemIndex == 2)
        {
            SetDefaults();
            Debug.Log("페널티 해제");
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
    
    
    public bool CheckPenalty(KeyCode key, float increase = 1.0f, float threshold = 3.0f, float decayRate = 2.0f) // 특정 키에 대한 열기(연타 횟수) 증가율, 임계치, 감소율 
    {
        if (!keyHeat.ContainsKey(key)) keyHeat[key] = 0f; // 키가 없으면 키 추가하고 초기 밸류 0으로 설정

        keyHeat[key] = Mathf.Max(0, keyHeat[key] - (decayRate * Time.deltaTime)); // 초당 감소율에 따라 열기 감소

        if (Input.GetKeyDown(key))
        {
            keyHeat[key] += increase; // 키 입력시 열기 증가
        }

        return keyHeat[key] >= threshold; // 임계치를 초과하면 true 반환
    }

    void ApplyPenalty()
    {
        defaultB = 25;
        defaultA = 5;
    }
    
    void SetDefaults()
    {
        defaultB = 65;
        defaultA = 15;
    }
}
