using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    public bool isBroken;
    public string interactionName;
    public int engineID;
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

        if (isBroken)
        {
            dmgParticle.SetActive(true);
            var triggerObject = GetComponent<Trigger>();
            triggerObject.enabled = true;
        }
    }



    public virtual void Repair()
    {
        dmgParticle.SetActive(false);
        isBroken = false;
    }

    public virtual void SpeedControl()
    {

    }
    public virtual void EnginePower() 
        {

        } 

}
