using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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
    private SFXManager sFXManager;
    private GameManager gameManager;
    public Transform shakeyTransform;

    private bool isWalking = false;

    HashSet<GameObject> destroyedGameObjects;
    
    
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCamera = GetComponentInChildren<Camera>();
        Debug.Log(playerCamera.name);
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        playerRigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sFXManager = FindAnyObjectByType<SFXManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        destroyedGameObjects = new HashSet<GameObject>();
        ShakeyCam();
    }

    void ShakeyCam()
    {
        Vector3 rotation = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.value;
        Vector3 rotationFixed = new Vector3(-rotation.x, -rotation.y, rotation.z);
        rotation = rotationFixed;
        shakeyTransform.rotation.ToAngleAxis(out float angle, out Vector3 axis);
        Vector3 currRotation = axis * angle;
        if ((currRotation + rotation).magnitude < 30)
        {
            shakeyTransform.Rotate(rotation);
        }
    }

    void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
    }
    //void DestroyAndAddToDestroyedList()
    //{
    //    destroyedGameObjects.Add(hit.collider.gameObject);
    //    Destroy(hit.collider.gameObject);
    //    sFXManager.PlayHitSound();
    //}
    void CheckRayCollision(Ray ray)
    {
        RaycastHit hit;
        float maximumDistanceOfRay = 5;
        if (Physics.Raycast(ray, out hit, maximumDistanceOfRay))
        {
            if (hit.collider.CompareTag("Mob") && !destroyedGameObjects.Contains(hit.collider.gameObject))
            {
                hit.collider.GetComponent<UnbrokenObjects>().IsAttacked();
                sFXManager.PlayFemmeAvSound();
            }
            else if (hit.collider.CompareTag("Props") && !destroyedGameObjects.Contains(hit.collider.gameObject) && gameManager.PropsDestroyable)
            {
                Debug.Log("Prop is hit");
                hit.collider.GetComponent<UnbrokenObjects>().IsAttacked();
            }
            else if (hit.collider.CompareTag("Furniture") && !destroyedGameObjects.Contains(hit.collider.gameObject) && gameManager.FurnitureDestroyable)
            {
                hit.collider.GetComponent<UnbrokenObjects>().IsAttacked();
            }
            else if (hit.collider.CompareTag("Walls") && !destroyedGameObjects.Contains(hit.collider.gameObject) && gameManager.WallsDestroyable)
            {
                hit.collider.GetComponent<UnbrokenObjects>().IsAttacked();
            }
            else if (hit.collider.CompareTag("Floor") && !destroyedGameObjects.Contains(hit.collider.gameObject) && gameManager.FloorDestroyable)
            {
                hit.collider.GetComponent<UnbrokenObjects>().IsAttacked();
            }
            else if (hit.collider.CompareTag("Walls") && !destroyedGameObjects.Contains(hit.collider.gameObject) && gameManager.WallsDestroyable)
            {
                hit.collider.GetComponent<UnbrokenObjects>().IsAttacked();
            }
            else if (hit.collider.CompareTag("Floor") && !destroyedGameObjects.Contains(hit.collider.gameObject) && gameManager.FloorDestroyable)
            {
                hit.collider.GetComponent<UnbrokenObjects>().IsAttacked();
            }
        }
    }

    void OnAttack(InputValue value)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack Swing"))
        {
            animator.Play("Attack Swing");
            sFXManager.PlaySwingSound();
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
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0; // Flatten the forward vector
        forward.Normalize(); // Normalize to maintain direction
        if (movementDirection.x != 0 || movementDirection.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        sFXManager.PlayWalkingSound(isWalking);

        Vector3 movement = new Vector3(-movementDirection.y, 0, movementDirection.x) * (movementSpeed);
        playerRigidBody.linearVelocity = movement;
        ShakeyCam();
    }

    void drawRay(Ray ray)
    {
        //ONLY VISIBLE IN GIZMOS VIEW
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
    }
}