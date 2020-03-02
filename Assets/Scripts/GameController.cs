using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float shipHealthMax = 20;

    public float shipHealth;
    public int currentScore;
    public int missionCompletedCount;
    public int missionTotalCount;


    [SerializeField]
    Slider healthSlider;

    [SerializeField]
    [Range(1, 5)]
    private float healthDepleteRate = 0.4f;

    private bool gameOver;
    [SerializeField]
    private GameObject g_EndScreen;
    [SerializeField]
    private TMP_Text t_Score, t_Mission, t_Title;

    private int score;


    [SerializeField]
    private int maxTimer, maxSeconds;

    private float currentTimer;

    [SerializeField]
    private TMP_Text t_Timer;

    private int timerMinutes;
    private float timerSeconds = 0;

    public float countDownModifier = 1.0f;

    public GameController[] gameControllers;

    private float distancedToIsland = 250f;
    public float engineOneSpeed, engineTwoSpeed;
    public float speed;

    [SerializeField]
    private GameObject islandObj;
    [SerializeField]
    private GameObject ship;

    [SerializeField]
    private Canvas pauseCanvas;
    [SerializeField]
    Button pauseButton;


    private void Awake()
    {
        shipHealth = shipHealthMax;
        timerMinutes = maxTimer;
        timerSeconds = maxSeconds;
        Time.timeScale = 1;

        gameControllers = FindObjectsOfType<GameController>();
        islandObj = GameObject.FindWithTag("Island");
        ship = GameObject.FindWithTag("Ship");

    }
    private void Start()
    {
        float dis = Vector3.Distance(islandObj.transform.position, ship.transform.position);
        dis += distancedToIsland;
        islandObj.transform.position = new Vector3(islandObj.transform.position.x, islandObj.transform.position.y, -distancedToIsland - 2500);
    }

    private void Update()
    {
        //if (gameOver)
        //    Time.timeScale = 0;
        TravelReduce();

        DepleteShipHealth();
        
    }


    void DepleteShipHealth ()
    {

        if (shipHealth <= 0 )
        {
            gameOver = true;
            g_EndScreen.SetActive(true);
            t_Score.text = "SUNK YOU LOSE!" ;
            t_Mission.text = "MISSION COMPLETED: " + missionCompletedCount.ToString() + " : " + missionTotalCount.ToString();
            return;
        }

        shipHealth -= healthDepleteRate * Time.deltaTime;

        healthSlider.value = shipHealth / shipHealthMax;

    }

    private void CountDownTimer ()
    {
        timerSeconds -= countDownModifier * Time.deltaTime;

        if (timerSeconds <= 0 && timerMinutes > 0)
        {
            timerMinutes--;
            timerSeconds = 60; 
        }

        string seconds = "";

        if (timerSeconds >= 10.0f )
            seconds = Mathf.FloorToInt(timerSeconds).ToString("F0");
        else if (timerSeconds < 10.0f)
            seconds = "0" + Mathf.FloorToInt(timerSeconds).ToString("F0");

        if (timerMinutes > 0)
            t_Timer.text = timerMinutes.ToString("F0") + ":" + seconds;
        else
            t_Timer.text = timerSeconds.ToString("F0") + seconds;

        if (timerSeconds <= 0 && timerMinutes <= 0)
        {
            t_Score.text = "YOU MADE IT!";
            t_Mission.text = "MISSION COMPLETED: " + missionCompletedCount.ToString() + " / " + missionTotalCount.ToString();
        }
        

        
    }

    public void MissionCountUp (bool success)
    {
        float shipRestore = 0.25f;

        if (success)
        {
            AddToScore(500);
            missionCompletedCount++;
            shipHealth += shipRestore * shipHealthMax;
            shipHealth = Mathf.Min(shipHealth, shipHealthMax);
        }
        else
            shipHealth -= shipRestore * shipHealth;

        missionTotalCount++;

    }

    public void TravelReduce()
    {
        if (distancedToIsland > 0)
        {
            speed = engineOneSpeed + engineTwoSpeed;
            distancedToIsland -= speed * Time.deltaTime;
            islandObj.transform.position = Vector3.MoveTowards(islandObj.transform.position, ship.transform.position, Time.deltaTime);

        }
        t_Timer.text = distancedToIsland.ToString("f2");
        

        if (distancedToIsland <= 0 && g_EndScreen.activeSelf == false)
        {
            Time.timeScale = 0;
            AddToScore(2000);
            AddToScore(Mathf.CeilToInt(shipHealth));
            t_Score.text = "TOTAL SCORE: " + score.ToString();
            t_Title.text = "YOU MADE IT!";
            t_Mission.text = "MISSION COMPLETED: " + missionCompletedCount.ToString() + " OF " + missionTotalCount.ToString();
            g_EndScreen.SetActive(true);
        }


    }

    public void AddToScore (int add)
    {
        score += add;
    }

    public void PauseGame()
    {
        if (gameOver)
            return;
     
        
        if (Time.timeScale == 1) // Pause game
        {
            pauseCanvas.enabled = true;
            
            Time.timeScale = 0;
        }
        else // Resume game
        {
            pauseCanvas.enabled = false;
            Time.timeScale = 1;
        }
    }





}
