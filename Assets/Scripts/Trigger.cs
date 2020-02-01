using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


    [RequireComponent(typeof(Breakable))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Canvas))]
   // [RequireComponent(typeof(ObjectBreak))]
    public class Trigger : MonoBehaviour
    {
        private Breakable interaction;
        private SphereCollider triggerCol;
        private Canvas textCanvas;
        public GameObject interactImage;
        public string interactionText;
        private float timer;
        private float interactDelay = 0.1f;
        

        private void Start()
        {
       
            interaction = GetComponent<Breakable>();
            triggerCol = GetComponent<SphereCollider>();
            textCanvas = GetComponent<Canvas>();
            
            interactionText = interaction.interactionName;
            textCanvas.enabled = false;

            triggerCol.isTrigger = true;
            //interactText.text = interactionText;
            timer = interactDelay;
        if(this.tag == "Breakable")
        {
            this.enabled = false;
        }
        else if(this.tag == "Engine")
        {
            this.enabled = true;
        }
            
    }
        private void Update()
        {
            timer += Time.deltaTime;
        }


        private void OnTriggerEnter(Collider other)
        {
            if(this.transform.tag == "Breakable")
            {
                if (other.tag == "Player1" && interaction.isBroken == true)
                 {
                textCanvas.enabled = true;
                interactImage.SetActive(true);
                 }
                else if (other.tag == "Player2" && interaction.isBroken == true)
                {
                textCanvas.enabled = true;
                interactImage.SetActive(true);
            }
            }

        if (this.transform.tag == "Engine")
        {
            if (other.tag == "Player1" && interaction.engineID == 0)
            {
                textCanvas.enabled = true;
                interactImage.SetActive(true);
            }
            else if (other.tag == "Player2" && interaction.engineID == 1)
            {
                textCanvas.enabled = true;
                interactImage.SetActive(true);
            }
        }


    }


        private void OnTriggerStay(Collider other)
        {
        if (this.transform.tag == "Breakable")
        {
            if (other.tag == "Player1" && Input.GetButtonDown("A1") && timer >= interactDelay && interaction.playerRef == 1)
            {
                var player = other.GetComponent<MovementInput>();
                player.anim.SetTrigger("Fixing");
                timer = 0;
                interaction.Repair(player);
                //interactText.text = "";
            }
            else if (other.tag == "Player2" && Input.GetButtonDown("A2") && timer >= interactDelay && interaction.playerRef == 2)
            {
                var player = other.GetComponent<MovementInput>();
                player.anim.SetTrigger("Fixing");
                timer = 0;
                interaction.Repair(player);
                //interactText.text = "";
            }
        }

        else if (this.tag == "Engine")
        {
            if (other.tag == "Player1" && Input.GetButtonDown("Y1") && timer >= interactDelay && interaction.engineID == 0)
            {
                timer = 0;
                interaction.SpeedControl();
                //interactText.text = "";
            }
            else if (other.tag == "Player2" && Input.GetButtonDown("Y2") && timer >= interactDelay && interaction.engineID == 1)
            {
                timer = 0;
                interaction.SpeedControl();
                //interactText.text = "";
            }
            else if(other.tag == "Player1" && Input.GetButtonDown("B1") && timer >= interactDelay && interaction.engineID == 0)
            {
                timer = 0;
                interaction.EnginePower();
                Debug.Log("B Pressed");
            }
            else if (other.tag == "Player2" && Input.GetButtonDown("B2") && timer >= interactDelay && interaction.engineID == 1)
            {
                timer = 0;
                interaction.EnginePower();
            }
        }

           
    }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player1" || other.tag == "Player2")
            {
                textCanvas.enabled = false;
            interactImage.SetActive(false);
        }
        }

    }


