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
    }

    void getRandomGrades()
    {
        if(currentItem != null) Destroy(currentItem);
        int randomNumber;
        randomNumber = Random.Range(0, 100);
        if(randomNumber < 65) itemIndex = 1; // B 65%
        else if(randomNumber < 80) itemIndex = 0; // A 15%
        else if(randomNumber < 100) itemIndex = 2; // F 20%
        currentItem = Instantiate(grades[itemIndex], Vector2.zero, Quaternion.identity);
    }

    private bool isProcessing = false;

    public void ProcessScore(bool isLP)
    {
        if (isProcessing) return;
        isProcessing = true;

        int score = 0;
        switch (itemIndex)
        {
            case 0: score = 3; break;
            case 1: score = 1; break;
            case 2: score = -2; break;
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

        Invoke("ResetProcessing", 0.1f); 
    }

    void ResetProcessing()
    {
        isProcessing = false;
    }
}