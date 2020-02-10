using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBreak : MonoBehaviour
{
    [SerializeField]
    private float timer;

    [SerializeField]
    private int objectID;

    [SerializeField]
    private float timeVal;

    [SerializeField]
    private Image toolSprite;

    private int selectedObject;
    public bool alreadyBroken = false;

    private void Awake()
    {
        if (objectID == 0)
        {
            Debug.Log("Please assign this object a ID greater than 0");
            timer = timeVal;
        }

        

    }

    private void Update()
    {
        if (alreadyBroken)
            return;

        //timer -= Time.deltaTime;
        //if(timer <= 0)
        //{
        //    var tempBreak = GetComponent<Breakable>();
        //    CauseDamage(tempBreak.engine.engineID); //checks the engine ID assigned in inspector;
        //}
    }


    public void BreakPart ()
    {
        // Break values.
        var tempBreak = GetComponent<Breakable>();
        tempBreak.Break(true);
        alreadyBroken = true;

    }

    private void CauseDamage(int engineID)
    {
        if (engineID == 0)
        {
            selectedObject = Random.Range(1, 7); //Rolls between the assigned 1 - 6 if the ID in inspector is 0 or the red engine
        } else if (engineID == 1)
        {
            selectedObject = Random.Range(7, 13); // Rolls between the assigned 7 - 12 if the ID in inspector is 1 or the blue engine
        }
        if (selectedObject == objectID)
        {
            var tempBreak = GetComponent<Breakable>();
            tempBreak.Break(true);
            alreadyBroken = true;
            timer = timeVal;
        }
        else
        {
            timer = timeVal;

        }
       
    }

    
}
