using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour
{

    public int playerNo;

    public float velocity;
    [Space]

    public float InputX;
    public float InputZ;
    public float jumpSpeed;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation = 0.1f;

    public Camera cam;
    public CharacterController controller;

    public int toolSelected;

    public AudioClip[] hitSoundEffects;
    AudioSource audio;

    public Transform player;


    public Transform cameraFixPlayer;

    public bool isGrounded;


    public bool isHolding;






    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;


    private float verticalVel;
    private Vector3 moveVector;
    public bool canMove;

    public float gravity = 20f;

    [SerializeField]
    private Transform emergencySpawn;


    void Start()
    {

        audio = GetComponent<AudioSource>();
        anim = player.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause" + playerNo))
        {
            FindObjectOfType<GameController>().PauseGame();
            Debug.Log("Pressed");
        }
        if (!canMove)
            return;


        InputMagnitude();

    }

    void PlayerMoveAndRotation()
    {
        if (gameObject.tag == "Player1")
        {
            InputX = Input.GetAxis("Horizontal");
            InputZ = Input.GetAxis("Vertical");
        }
        else if (gameObject.tag == "Player2")
        {
            InputX = Input.GetAxis("Horizontal1");
            InputZ = Input.GetAxis("Vertical1");
        }

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (blockRotationPlayer == false)
        {

            if (controller.isGrounded)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    desiredMoveDirection.y = jumpSpeed;
                }

            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            }

            desiredMoveDirection.y -= gravity;
            controller.Move(desiredMoveDirection * Time.deltaTime * velocity);

        }


    }

    public void RotateToCamera(Transform t)
    {


        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

    public void RotateTowards(Transform t)
    {


        transform.rotation = Quaternion.LookRotation(t.position - transform.position);

    }

    void InputMagnitude()
    {


        if (gameObject.tag == "Player1")
        {
            InputX = Input.GetAxis("Horizontal");
            InputZ = Input.GetAxis("Vertical");
        }
        else if (gameObject.tag == "Player2")
        {
            InputX = Input.GetAxis("Horizontal1");
            InputZ = Input.GetAxis("Vertical1");
        }
        anim.SetFloat("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
        anim.SetFloat("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

        //Calculate the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player
        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, StartAnimTime, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, StopAnimTime, Time.deltaTime);
        }

    }

    public void PlaySoundEffect()
    {
        audio.PlayOneShot(hitSoundEffects[toolSelected -1]);
    }

    
    


}
