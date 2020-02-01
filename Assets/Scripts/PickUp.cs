using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Obtainable))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Canvas))]
public class PickUp : MonoBehaviour
{
    private Obtainable obtainObject;
    private SphereCollider triggerCol;
    private Canvas buttonCanvas;
    private float timer;
    private float interactDelay = 0.1f;
    public Image xButton;

    [SerializeField]
    private bool toolAvailable = true; // Start of the game.


    private void Start()
    {
        obtainObject = GetComponent<Obtainable>();
        triggerCol = GetComponent<SphereCollider>();
        buttonCanvas = GetComponent<Canvas>();
        triggerCol.isTrigger = true;
        buttonCanvas.enabled = false;
        xButton.enabled = false;
        timer = interactDelay;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {

            buttonCanvas.enabled = true;
            xButton.enabled = true;
        }
        else if (other.tag == "Player2")
        {
            buttonCanvas.enabled = true;
            xButton.enabled = true;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player1" && Input.GetButtonDown("X1") && timer >= interactDelay && this.isActiveAndEnabled)
        {
            timer = 0;
            obtainObject.GrabObjectPlayer1(other.GetComponent<MovementInput>());
            

            
        }
        else if (other.tag == "Player2" && Input.GetButtonDown("X2") && timer >= interactDelay && this.isActiveAndEnabled)
        {
            timer = 0;
            obtainObject.GrabObjectPlayer2( other.GetComponent<MovementInput>());
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            buttonCanvas.enabled = false;
            xButton.enabled = false;
        }
    }
}
