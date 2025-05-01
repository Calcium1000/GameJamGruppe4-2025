using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Behavior : MonoBehaviour
{
    private GameObject attackHand;
    private PlayerInput playerInput;
    private Animator animator;
    private Camera playerCamera;
    private InputSystem_Actions inputSystem;
    private Vector2 moveInput;
    private float movementSpeed = 10f;
    private SFXManager sfxManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCamera = GetComponentInChildren<Camera>();
        Debug.Log(playerCamera.name);
        inputSystem = new InputSystem_Actions();
        animator = GetComponent<Animator>();
        sfxManager = new SFXManager();
    }
    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnAttack(InputValue value)
    {
        sfxManager.PlaySwingSound();
    }
    // Update is called once per frame
    void Update()
    {
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            transform.position += new Vector3(moveInput.y, 0, moveInput.x) * (movementSpeed * Time.deltaTime);

        }
    }

    void drawRay(Ray ray)
    {
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 5f);
    }
}
