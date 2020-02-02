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
    private float healthDepleteRate;

    private bool gameOver;
    [SerializeField]
    private GameObject g_EndScreen;
    [SerializeField]
    private TMP_Text t_Score, t_Mission;


    [SerializeField]
    private int maxTimer, maxSeconds;

    private float currentTimer;

    [SerializeField]
    private TMP_Text t_Timer;

    private int timerMinutes;
    private float timerSeconds = 0;

    public float countDownModifier = 1.0f;

    

    


    private void Awake()
    {
        DontDestroyOnLoad(this);
        shipHealth = shipHealthMax;
        timerMinutes = maxTimer;
        timerSeconds = maxSeconds;

    }

    private void Update()
    {
        if (gameOver)
            Time.timeScale = 0;

        DepleteShipHealth();
        CountDownTimer();
    }


    void DepleteShipHealth ()
    {

        if (shipHealth <= 0 )
        {
            gameOver = true;
            g_EndScreen.SetActive(true);
            t_Score.text = "Score: " + currentScore.ToString();
            t_Mission.text = "Mission Completed: " + missionCompletedCount.ToString() + " / " + missionTotalCount.ToString();
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
        

        
    }

    public void MissionCountUp (bool success)
    {
        float shipRestore = 0.5f;

        if (success)
        {
            missionCompletedCount++;
            shipHealth += shipRestore * shipHealth/100;
            shipHealth = Mathf.Min(shipHealth, shipHealthMax);
        }
        else
            shipHealth -= shipRestore * shipHealth;

        missionTotalCount++;

    }

}
