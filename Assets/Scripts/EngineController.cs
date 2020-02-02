using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineController : Breakable
{

    bool engineOn = false;
    bool isFast = false;

    public int currentState = 1;
    
    public Animator engineAnim;

    private MissionControls missionControls;

    

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
        }
        else if (isFast)
        {
            currentState = 1;
            engineAnim.SetBool("IsFast", false);
            isFast = false;
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

        }
        else if (!engineOn)
        {
            Debug.Log("Engine on");
            currentState = 0;
            engineAnim.SetBool("PowerDown", false);
            engineOn = true;
        }
        missionControls.ComapareMissionRequirements();

    }



}
