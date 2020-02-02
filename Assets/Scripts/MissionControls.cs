using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        engineControllers = FindObjectsOfType<EngineController>();
    }

    private void Start()
    {
        missionTimerCounter = missionBegunTimer;
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
                SetEngineRequirements();
                ComapareMissionRequirements();
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

        if (engineControllers[0].currentState == reqOne && !engineControllers[0].isBroken)
            conditionOneMet = true;
        else if (engineControllers[0].currentState != reqOne)
            conditionOneMet = false;

        if (engineControllers[1].currentState == reqTwo)
            conditionTwoMet = true;
        else if (engineControllers[1].currentState != reqTwo && !engineControllers[1].isBroken)
            conditionTwoMet = false;


        if (conditionOneMet == true && conditionTwoMet == true)
            missionCompleted = true;

    }



}
