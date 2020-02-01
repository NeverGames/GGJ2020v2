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
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            CauseDamage();
        }
    }

    private void CauseDamage()
    {
            int selectedObject = Random.Range(1, 7);
            if (selectedObject == objectID)
            {
                var tempBreak = GetComponent<Breakable>();
                tempBreak.Break(true);
            }
            else
            {
                timer = timeVal;

            }
       
    }

    
}
