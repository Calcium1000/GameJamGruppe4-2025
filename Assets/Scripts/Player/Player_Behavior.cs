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
    protected PlayerInput playerInput;
    public Rigidbody myBody; // Her er det rigidbody man flytter rundt på
    private Animator animator;
    private Camera playerCamera;
    protected InputSystem_Actions inputSystem;
 
    private SFXManager sfxManager;
    private GameManager gameManager;

    protected bool isWalking = false;

    HashSet<GameObject> destroyedGameObjects;
    
    
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCamera = GetComponentInChildren<Camera>();
        Debug.Log("playerCam: " + playerCamera.name);
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        animator = GetComponent<Animator>();
        sfxManager = FindAnyObjectByType<SFXManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        destroyedGameObjects = new HashSet<GameObject>();
        myBody = GetComponent<Rigidbody>();
    }

    

    void CheckRayCollision(Ray ray)
    {
        RaycastHit hit;
        float maximumDistanceOfRay = 5;
        void DestroyAndAddToDestroyedList()
        {
            destroyedGameObjects.Add(hit.collider.gameObject);
            Destroy(hit.collider.gameObject);
            sfxManager.PlayHitSound();
        }
        if (Physics.Raycast(ray, out hit, maximumDistanceOfRay))
        {
            if (hit.collider.CompareTag("Mob") && !destroyedGameObjects.Contains(hit.collider.gameObject))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent<UnbrokenObjects>(out UnbrokenObjects unbrokenObjects))
                {
                    unbrokenObjects.isAttacked();
                }
                DestroyAndAddToDestroyedList();
                sfxManager.PlayFemmeAvSound();
            }
            else if (hit.collider.CompareTag("Furniture") && !destroyedGameObjects.Contains(hit.collider.gameObject) && gameManager.FloorDestroyable)
            {
                DestroyAndAddToDestroyedList();
            }
            else if (hit.collider.CompareTag("Walls") && !destroyedGameObjects.Contains(hit.collider.gameObject) && gameManager.WallsDestroyable)
            {
                DestroyAndAddToDestroyedList();
            }
            else if (hit.collider.CompareTag("Floor") && !destroyedGameObjects.Contains(hit.collider.gameObject) && gameManager.FloorDestroyable)
            {
                DestroyAndAddToDestroyedList();
            }
        }
    }

    void OnAttack(InputValue value)
    {
        Debug.Log("Is attacking");
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack Swing"))
        {
            animator.Play("Attack Swing");
            //sfxManager.PlaySwingSound();
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
        sfxManager.PlayWalkingSound(isWalking);
    }

    void drawRay(Ray ray)
    {
        //ONLY VISIBLE IN GIZMOS VIEW
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
    }
}