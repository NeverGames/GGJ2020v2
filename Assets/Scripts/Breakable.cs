using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    public bool isBroken;
    public string interactionName;
    
    public int playerRef;
    public GameObject dmgParticle;

    public EngineController engine;

    private void Start()
    {
        if(dmgParticle != null)
        {
            dmgParticle.SetActive(false);
        }

        if(GetComponent<ObjectBreak>() != null)
            engine.objectBreaks.Add(GetComponent<ObjectBreak>());
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
            engine.engineBroken = true;
        }
    }



    public virtual void Repair(MovementInput player)
    {
        if (GetComponent<ToolSelect>().selectID == player.toolSelected)
        {
            dmgParticle.SetActive(false);
            isBroken = false;
            engine.engineBroken = false;
            FindObjectOfType<GameController>().AddToScore(100);

            GetComponent<ObjectBreak>().alreadyBroken = false;
        }
        
    }

    

}
