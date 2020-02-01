using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    public bool isBroken;
    public string interactionName;
    public int engineID;
    public int playerRef;
    public GameObject dmgParticle;



    private void Start()
    {
        if(dmgParticle != null)
        {
            dmgParticle.SetActive(false);
        }
        
    }
    public virtual void InteractWith()
    {

    }


    public virtual void Break(bool breakBool)
    {
        isBroken = breakBool;
        var toolReq = GetComponent<ToolSelect>();
        int tempint = Random.Range(1, 4); 
        toolReq.PickTool(tempint);
        if (isBroken)
        {
            dmgParticle.SetActive(true);
            var triggerObject = GetComponent<Trigger>();
            triggerObject.enabled = true;
        }
    }



    public virtual void Repair(MovementInput player)
    {
        if (GetComponent<ToolSelect>().selectID == player.toolSelected)
        {
            dmgParticle.SetActive(false);
            isBroken = false;

            GetComponent<ObjectBreak>().alreadyBroken = false;
        }
        
    }

    public virtual void SpeedControl()
    {

    }
    public virtual void EnginePower() 
        {

        } 

}
