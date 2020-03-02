using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EngineController : MonoBehaviour
{
    public int engineID;
    bool engineOn = true;
    bool isFast = false;

    public int currentState = 1;
    private int lastState;

    public Animator engineAnim;

    private MissionControls missionControls;
    private GameController gameController;

    [SerializeField]
    private Image currentModeImage;

    public List<ObjectBreak> objectBreaks;

    public bool engineBroken;

    private float brakeChanceMuiltiplier = 1.0f;

    private float rngTimer;

    private void Awake()
    {
        missionControls = FindObjectOfType<MissionControls>();
        gameController = FindObjectOfType<GameController>();
        currentState = 1;

        rngTimer = Random.Range(10, 15);
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //    CauseDamage();

        rngTimer -= Time.deltaTime;

        if (rngTimer <= 0)
        {
            CauseDamage();
            rngTimer = Random.Range(10, 15);
        }
        SpeedDistanceToIsland();
    }

    public void SpeedControl()
    {
        //base.SpeedControl();
        if (engineBroken)
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

        missionControls.CompareMissionRequirements();

    }

    public void EnginePower()
    {
        if (engineBroken)
            return;
        //base.EnginePower();
        if (engineOn)
        {
            Debug.Log("Engine off");
            engineAnim.SetBool("PowerDown",true);
            engineOn = false;
            currentModeImage.sprite = missionControls.missionIcons[0];
            lastState = currentState;
            currentState = 0;

        }
        else if (!engineOn)
        {
            Debug.Log("Engine on");
            
            engineAnim.SetBool("PowerDown", false);
            engineOn = true;
            currentModeImage.sprite = missionControls.missionIcons[lastState];
            currentState = lastState;
            

        }
        missionControls.CompareMissionRequirements();

    }

    private void CauseDamage()
    {
        // Chance of breaking.
        int rng = Random.Range(0, 100);

        brakeChanceMuiltiplier = 0;
        for (int i = 0; i < objectBreaks.Count; i++)
        {
            if (objectBreaks[i].alreadyBroken == false)
                brakeChanceMuiltiplier += 0.1f;
        }

        // Debug.Log("brakemuiltplier: " + brakeChanceMuiltiplier);
        int chance = Mathf.CeilToInt (100 * brakeChanceMuiltiplier);
        //SDebug.Log("Chance: " + chance + " rng: " + rng);
        if (rng < chance)
        {
            int o = ObjectBreakNumber();
            if(o != -1)
                objectBreaks[ObjectBreakNumber()].BreakPart();
            
        }
    }

    private int ObjectBreakNumber ()
    {
        int failcheck = 0;
        while (true)
        {
            int obj = Random.Range(0, objectBreaks.Count);

            if (objectBreaks[obj].alreadyBroken == false)
                return obj;

            failcheck++;

            if (failcheck > 10)
            {
                Debug.Log("forced to check");
                for (int i = 0; i < objectBreaks.Count; i++)
                {
                    if (objectBreaks[i].alreadyBroken == false)
                        return i;
                }
                return -1;
            }
        }
    }

    private void SpeedDistanceToIsland ()
    {
        if (currentState == 0)
        {
            // no movement
        }
        else if(currentState == 1)
        {
            if (engineID == 0)
                gameController.engineOneSpeed = 0.75f;
            else if (engineID == 1)
                gameController.engineOneSpeed = 0.75f;
        }
        else if (currentState == 2)
        {
            if (engineID == 0) 
                gameController.engineOneSpeed = 1.5f;
            else if (engineID == 1)
                gameController.engineOneSpeed = 1.5f;
        }
    }

}
