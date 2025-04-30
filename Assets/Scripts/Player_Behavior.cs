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
    private InputSystem_Actions inputSystem;
    private float movementSpeed = 10f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCamera = GetComponentInChildren<Camera>();
        Debug.Log(playerCamera.name);
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        animator = GetComponent<Animator>();


    }

    void OnMove(InputValue value)
    {

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
    }

    void drawRay(Ray ray)
    {
        //ONLY VISIBLE IN GIZMOS VIEW
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
    }
}
