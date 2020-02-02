﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionControls : MonoBehaviour
{
    [SerializeField]
    private int reqOne, reqTwo;
    [SerializeField]
    bool conditionOneMet = false;
    [SerializeField]
    bool conditionTwoMet = false;

    bool missionStart;
    [SerializeField]
    private bool missionCompleted = false;
    [SerializeField]
    private int missionChance;

    private float missionTimerChance = 5f;
    private float missionBegunTimer = 15.0f;
    [SerializeField]
    private float missionTimerCounter;

    [SerializeField]
    EngineController[] engineControllers;


    public GameObject chief;
    public TMP_Text MissionText;
    public GameObject speakEffect;

    private void Awake()
    {
        engineControllers = FindObjectsOfType<EngineController>();
    }

    private void Start()
    {
        missionTimerCounter = missionBegunTimer;
        speakEffect.SetActive(false);
        MissionText.enabled = false;
    }

    private void Update()
    {
        if (!missionStart)
            ChanceMissionStart();


        missionTimerCounter -= Time.deltaTime;


        if (missionTimerCounter <= 0 )
        {
            FindObjectOfType<GameController>().MissionCountUp(missionCompleted);
            missionStart = false;
            missionCompleted = false;
            conditionOneMet = false;
            conditionTwoMet = false;

            missionTimerChance = 10.0f;
            missionTimerCounter = missionBegunTimer;


        }


        
    }

    private void ChanceMissionStart ()
    {
        missionTimerChance -= Time.deltaTime;
        if (missionTimerChance <= 0)
        {
            int rng = Random.Range(0, 10);
            if (rng <= missionChance)
            {
                missionStart = true;
                speakEffect.SetActive(true);
                var chiefAnim = chief.GetComponent<Animator>();
                chiefAnim.SetTrigger("IsCommanding");
                SetEngineRequirements();
                ComapareMissionRequirements();
                DisplayMission();
            }
            else
                missionTimerChance = 10.0f;

        }

    }

    private void SetEngineRequirements ()
    {
        reqOne = Random.Range(0, 3);
        reqTwo = Random.Range(0, 3);

    }

    public void ComapareMissionRequirements ()
    {

        if (engineControllers[0].currentState == reqOne)
            conditionOneMet = true;
        else if (engineControllers[0].currentState != reqOne)
            conditionOneMet = false;

        if (engineControllers[1].currentState == reqTwo)
            conditionTwoMet = true;
        else if (engineControllers[1].currentState != reqTwo)
            conditionTwoMet = false;


        if (conditionOneMet == true && conditionTwoMet == true)
        {
            missionCompleted = true;
            MissionText.text = "";
            MissionText.enabled = false;
        }
            

    }

    private void DisplayMission ()
    {
       

        List<string> stringTypes = new List<string>();

        stringTypes.Add("Stop ");
        stringTypes.Add("Slow Down ");
        stringTypes.Add("Speed up ");
        stringTypes.Add("Cruise ");

        string order1 = "";
        string order2 = "";

        for (int i = 0; i < stringTypes.Count; i++)
        {
            if (reqOne == i)
                order1 = stringTypes[i];

            if (reqTwo == i)
                order2 = stringTypes[i];

        }

        if (conditionOneMet)
            order1 = stringTypes[stringTypes.Count -1];
        if (conditionTwoMet)
            order2 = stringTypes[stringTypes.Count -1];


        string display = "Urgent! Red must " + order1 + "Blue must " + order2;
        MissionText.enabled = true;
        MissionText.text = display;
        Debug.Log(display);
    }


   

}
