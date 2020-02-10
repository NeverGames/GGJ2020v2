﻿using System.Collections;
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
    private TMP_Text t_Score, t_Mission;


    [SerializeField]
    private int maxTimer, maxSeconds;

    private float currentTimer;

    [SerializeField]
    private TMP_Text t_Timer;

    private int timerMinutes;
    private float timerSeconds = 0;

    public float countDownModifier = 1.0f;

    public GameController[] gameControllers;

    private float distancedToIsland = 200f;


    private void Awake()
    {
        shipHealth = shipHealthMax;
        timerMinutes = maxTimer;
        timerSeconds = maxSeconds;
        Time.timeScale = 1;

        gameControllers = FindObjectsOfType<GameController>();

    }

    private void Update()
    {
        //if (gameOver)
        //    Time.timeScale = 0;
        

        DepleteShipHealth();
        //CountDownTimer();
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
            t_Score.text = "YOU WON!";
            t_Mission.text = "MISSION COMPLETED: " + missionCompletedCount.ToString() + " / " + missionTotalCount.ToString();
        }
        

        
    }

    public void MissionCountUp (bool success)
    {
        float shipRestore = 0.25f;

        if (success)
        {
            missionCompletedCount++;
            shipHealth += shipRestore * shipHealthMax;
            shipHealth = Mathf.Min(shipHealth, shipHealthMax);
        }
        else
            shipHealth -= shipRestore * shipHealth;

        missionTotalCount++;

    }

    public void TravelReduce(float speed)
    {
        if(distancedToIsland > 0)
            distancedToIsland -= speed * Time.deltaTime;
        t_Timer.text = distancedToIsland.ToString("f2");

        if (distancedToIsland <= 0)
        {
            g_EndScreen.SetActive(true);
        }


    }



}
