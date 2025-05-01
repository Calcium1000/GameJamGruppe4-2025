using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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
    private GameManager gameManager;

    private bool isWalking;

    HashSet<GameObject> hitMobs;
    
    
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCamera = GetComponentInChildren<Camera>();
        Debug.Log(playerCamera.name);
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        playerRigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sfxManager = FindAnyObjectByType<SFXManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        hitMobs = new HashSet<GameObject>();
    }

    void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
    }

    void CheckRayCollision(Ray ray)
    {
        RaycastHit hit;
        float maximumDistanceOfRay = 2;
        void CollideDestroyPlaySFX()
        {
            hitMobs.Add(hit.collider.gameObject);
            Destroy(hit.collider.gameObject);
            sfxManager.PlayFemmeAvSound();
        }
        if (Physics.Raycast(ray, out hit, maximumDistanceOfRay))
        {
            if (hit.collider.CompareTag("Femme mob") && !hitMobs.Contains(hit.collider.gameObject))
            {
                CollideDestroyPlaySFX();
            }
            else if (hit.collider.CompareTag("Masc mob") && !hitMobs.Contains(hit.collider.gameObject))
            {
                CollideDestroyPlaySFX();
            }
            else if (hit.collider.CompareTag("Furniture") && gameManager.FloorDestroyable)
            {
                CollideDestroyPlaySFX();
            }
            else if (hit.collider.CompareTag("Walls") && gameManager.WallsDestroyable)
            {
                CollideDestroyPlaySFX();
            }
            else if (hit.collider.CompareTag("Floor") && gameManager.FloorDestroyable)
            {
                CollideDestroyPlaySFX();
            }
        }
    }

    void OnAttack(InputValue value)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack Swing"))
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
                CheckRayCollision(shot);
            }
        }
    }

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
