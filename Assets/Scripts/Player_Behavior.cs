using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Behavior : MonoBehaviour
{
    private GameObject attackHand;
    private PlayerInput playerInput;
    private Animator animator;
    private Camera playerCamera;
    private Rigidbody playerRigidBody;
    private InputSystem_Actions inputSystem;
    private Vector2 movementDirection;
    private float movementSpeed = 10f;
    private SFXManager sfxManager;

    private bool isWalking;
    
    
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCamera = GetComponentInChildren<Camera>();
        Debug.Log(playerCamera.name);
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        playerRigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sfxManager = sfxManager = FindAnyObjectByType<SFXManager>();
    }

    void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        animator.Play("Attack Swing");
        sfxManager.PlaySwingSound();
        //shoot 10 rats in cone
        int numRays = 10;
        float deg = 10f;
        for (int i = 0; i < numRays; i++)
        {
            Ray shot = new Ray(playerCamera.transform.position, Quaternion.Euler(0, (i - (numRays / 2)) * deg, 0) * playerCamera.transform.rotation * new Vector3(0, 0, 1));
            drawRay(shot);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (movementDirection.x != 0 || movementDirection.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        sfxManager.PlayWalkingSound(isWalking);

        Vector3 movement = new Vector3(-movementDirection.y, 0, movementDirection.x) * (movementSpeed);
        playerRigidBody.linearVelocity = movement;
    }

    void drawRay(Ray ray)
    {
        //ONLY VISIBLE IN GIZMOS VIEW
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
    }
}
