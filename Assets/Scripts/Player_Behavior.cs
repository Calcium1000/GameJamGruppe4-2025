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
    private Vector2 movementDirection;
    private float movementSpeed = 10f;
    public SFXManager sfxManager;
    public GameManager gameManager;
    public float lookSpeed = 1000;
    private bool isWalking = false;

    HashSet<GameObject> destroyedGameObjects;
    public Transform shakeyTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destroyedGameObjects = new HashSet<GameObject>();
        playerCamera = GetComponentInChildren<Camera>();
        Debug.Log(playerCamera.name);
        playerRigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        try {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        } catch
        {
            Debug.Log("Gyroscope not available");
        }
    }

    void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
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
            if (hit.collider.CompareTag("Mob") & !destroyedGameObjects.Contains(hit.collider.gameObject))
            {
                DestroyAndAddToDestroyedList();
                sfxManager.PlayFemmeAvSound();
            }
            else if (hit.collider.CompareTag("Furniture") & !destroyedGameObjects.Contains(hit.collider.gameObject) & gameManager.FloorDestroyable)
            {
                DestroyAndAddToDestroyedList();
            }
            else if (hit.collider.CompareTag("Walls") & !destroyedGameObjects.Contains(hit.collider.gameObject) & gameManager.WallsDestroyable)
            {
                DestroyAndAddToDestroyedList();
            }
            else if (hit.collider.CompareTag("Floor") & !destroyedGameObjects.Contains(hit.collider.gameObject) & gameManager.FloorDestroyable)
            {
                DestroyAndAddToDestroyedList();
            }
        }
    }

    void OnAttack(InputValue value)
    {
        Debug.Log("Attack");
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

        Vector3 right = Camera.main.transform.right;
        right.y = 0; // Flatten the right vector
        right.Normalize(); // Normalize to maintain direction

        Vector3 movement = forward * movementDirection.y + right * movementDirection.x;
        movement *= movementSpeed;
        playerRigidBody.linearVelocity = movement;

        Vector2 lookDirection = GetComponent<PlayerInput>().actions.FindAction("Look").ReadValue<Vector2>();
        lookDirection *= lookSpeed * Time.deltaTime;
        Camera.main.transform.Rotate(new Vector3(-lookDirection.y, lookDirection.x, 0));
        // Lock the Z-axis to 0
        Vector3 eulerAngles = Camera.main.transform.eulerAngles;
        eulerAngles.z = 0;
        Camera.main.transform.eulerAngles = eulerAngles;
        try
        {
            ShakeyCam();

        } catch
        {
            Debug.Log("Gyroscope not available");
        }
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
    void drawRay(Ray ray)
    {
        //ONLY VISIBLE IN GIZMOS VIEW
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
    }
}