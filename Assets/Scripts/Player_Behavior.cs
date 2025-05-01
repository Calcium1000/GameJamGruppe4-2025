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
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCamera = GetComponentInChildren<Camera>();
        Debug.Log(playerCamera.name);
        playerRigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
    }

   

    void OnAttack(InputValue value)
    {
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

        Camera.main.transform.Rotate(new Vector3(-lookDirection.y, lookDirection.x, 0));
        // Lock the Z-axis to 0
        Vector3 eulerAngles = Camera.main.transform.eulerAngles;
        eulerAngles.z = 0;
        Camera.main.transform.eulerAngles = eulerAngles;
    }

    void drawRay(Ray ray)
    {
        //ONLY VISIBLE IN GIZMOS VIEW
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
    }
}
