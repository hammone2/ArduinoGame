using TMPro;
using UnityEngine;

public class CarSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI finalKillsText;
    [SerializeField] private GameObject blackFade;
    [SerializeField] private GameObject finalScoreUI;

    [Header("Scoring")]
    [SerializeField] private int pointsPerKill = 100;
    [SerializeField] private int timePenaltyPerSecond = 1;
    

    private float elapsedTime = 0f;
    private int kills = 0;
    private bool timerRunning = false;
    private int finalScore = 0;

    public static CarSceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateTimerText(0f);
    }

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText(elapsedTime);
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateKillsText()
    {
        kills++;
        killsText.text = "Kills: " + kills.ToString();
    }

    public void CalculateFinalScore()
    {
        int elapsedSeconds = Mathf.FloorToInt(elapsedTime);
        finalScore = Mathf.Max(0, kills * pointsPerKill - elapsedSeconds * timePenaltyPerSecond);

        blackFade.SetActive(true);
        blackFade.GetComponent<Animator>().SetTrigger("FadeOut");

        finalScoreUI.SetActive(true);
        finalScoreUI.GetComponent<Animator>().SetTrigger("ShowFinalScore");

        finalScoreText.SetText("Final Score: " + finalScore.ToString());
        finalTimeText.SetText("Time: " + Mathf.FloorToInt(elapsedTime / 60).ToString("00") + ":" + Mathf.FloorToInt(elapsedTime % 60).ToString("00"));
        finalKillsText.SetText("Kills: " + kills.ToString());
    }
}
