using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionControls : MonoBehaviour
{
    [SerializeField]
    private int reqOne, reqTwo;
    [SerializeField]
    bool conditionOneMet = false;
    [SerializeField]
    bool conditionTwoMet = false;
    [SerializeField]
    bool missionStart;
    [SerializeField]
    private bool missionCompleted = false;
    [SerializeField]
    private int missionChance;

    private float missionTimerChance = 5f;
    private float missionBegunTimer = 30.0f;
    [SerializeField]
    private float missionTimerCounter;

    [SerializeField]
    EngineController[] engineControllers;


    public GameObject chief;
    //public TMP_Text MissionText;
    public GameObject speakEffect;

    [SerializeField]
    private Slider missionSlider;
    [SerializeField]
    private GameObject missionSliderHolder;

    [SerializeField]
    private GameObject speachBubble;

    public Sprite[] missionIcons;

    [SerializeField]
    private Image redIcon, blueIcon;

    

    [SerializeField]
    private AudioClip completedSound, failSound;

    private AudioSource audio;


    private void Awake()
    {
        engineControllers = FindObjectsOfType<EngineController>();
        audio = GetComponent<AudioSource>();  
    }

    private void Start()
    {
        missionTimerCounter = missionBegunTimer;
        //speakEffect.SetActive(false);
        //MissionText.enabled = false;
        missionSliderHolder.SetActive(false);
        speachBubble.SetActive(false);
    }

    private void Update()
    {
        if (!missionStart)
        {
            ChanceMissionStart();
            return;
        }
        CompareMissionRequirements();
        missionTimerCounter -= Time.deltaTime;
        MissionSliderDisplay();

        if (missionTimerCounter <= 0  || missionCompleted)
        {
            
            

            if (missionCompleted)
                audio.PlayOneShot(completedSound);
            else
                audio.PlayOneShot(failSound);

            FindObjectOfType<GameController>().MissionCountUp(missionCompleted);
            missionStart = false;
            missionCompleted = false;

            conditionOneMet = false;
            conditionTwoMet = false;

            speachBubble.SetActive(false);
            missionSliderHolder.SetActive(false);

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
                missionSliderHolder.SetActive(true);
                
                //speakEffect.SetActive(true);
                var chiefAnim = chief.GetComponent<Animator>();
                chiefAnim.SetTrigger("IsCommanding");
                SetEngineRequirements();
                CompareMissionRequirements();
                missionTimerCounter = missionBegunTimer;
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

        CompareMissionRequirements();
        if (missionCompleted == true)
        {
            missionCompleted = false;
            missionStart = false;
            SetEngineRequirements();
        }
        else
            missionStart = true;

    }

    public void CompareMissionRequirements ()
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
            //MissionText.text = "";
            //MissionText.enabled = false;
        }
            

    }

    private void DisplayMission ()
    {

        speachBubble.SetActive(true);
        for (int i = 0; i < missionIcons.Length; i++)
        {
            if (reqOne == i)
                blueIcon.sprite = missionIcons[i];

            if (reqTwo == i)
                redIcon.sprite = missionIcons[i];
        }
       
    }

    private void MissionSliderDisplay ()
    {
        missionSlider.value = missionTimerCounter / missionBegunTimer;
    }


   

}
