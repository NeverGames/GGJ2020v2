using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineController : Breakable
{

    bool engineOn = true;
    bool isFast = false;
    public Animator engineAnim;

    public override void SpeedControl()
    {
        //base.SpeedControl();
        if (!isFast)
        {
            engineAnim.SetBool("IsFast", true);
            isFast = true;
        }
        else if (isFast)
        {
            engineAnim.SetBool("IsFast", false);
            isFast = false;
        }

        

    }

    public override void EnginePower()
    {

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
            engineAnim.SetBool("PowerDown", false);
            engineOn = true;

        } 
    }

}
