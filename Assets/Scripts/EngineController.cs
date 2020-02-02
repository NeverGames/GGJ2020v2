using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EngineController : Breakable
{

    bool engineOn = false;
    bool isFast = false;

    public int currentState = 1;
    
    public Animator engineAnim;

    private MissionControls missionControls;

    [SerializeField]
    private Image currentModeImage;


    private void Awake()
    {
        missionControls = FindObjectOfType<MissionControls>();
        currentState = 1;
    }


    public override void SpeedControl()
    {
        //base.SpeedControl();
        if (isBroken)
            return;
        
        if (!isFast)
        {
            currentState = 2;
            engineAnim.SetBool("IsFast", true);
            isFast = true;
            currentModeImage.sprite = missionControls.missionIcons[2];
        }
        else if (isFast)
        {
            currentState = 1;
            engineAnim.SetBool("IsFast", false);
            isFast = false;
            currentModeImage.sprite = missionControls.missionIcons[1];
        }

        missionControls.ComapareMissionRequirements();

    }

    public override void EnginePower()
    {
        if (isBroken)
            return;
        //base.EnginePower();
        if (engineOn)
        {
            Debug.Log("Engine off");
            engineAnim.SetBool("PowerDown",true);
            engineOn = false;
            currentModeImage.sprite = missionControls.missionIcons[0];

        }
        else if (!engineOn)
        {
            Debug.Log("Engine on");
            
            engineAnim.SetBool("PowerDown", false);
            engineOn = true;
            currentModeImage.sprite = missionControls.missionIcons[currentState];
            currentState = 0;

        }
        missionControls.ComapareMissionRequirements();

    }



}
