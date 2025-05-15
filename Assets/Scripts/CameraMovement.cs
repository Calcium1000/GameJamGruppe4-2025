using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : Player_Behavior
{
    [SerializeField] float sensitivity = 0.2f;

    private float xRotation;
    private float yRotation;
    private float yCeiling = 90f;
    private Vector2 lookInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        lookInput = new Vector3(-lookInput.y, lookInput.x, 0);
        // Handle look movement here
        // if (lookInput.x > 0)
        // {
        //     xRotation = 1;
        // }
        // else if (lookInput.x < 0)
        // {
        //     xRotation = -1;
        // }
        // else
        // {
        //     xRotation = 0;
        // }
        //
        // if (lookInput.y > 0)
        // {
        //     yRotation = 1;
        // }
        // else if (lookInput.y < 0)
        // {
        //     yRotation = -1;
        // }
        // else
        // {
        //     yRotation = 0;
        // }
        Debug.Log("Look Input: " + lookInput);
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = new Vector3(Mathf.LerpAngle(transform.rotation.x, lookInput.x, sensitivity), Mathf.LerpAngle(transform.rotation.y, lookInput.y, sensitivity), 0);
        transform.Rotate(currentRotation);
    }

    void FixedUpdate()
    {

    }
}
