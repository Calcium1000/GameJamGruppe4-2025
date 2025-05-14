using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
// Først skal man bruge "InputSystem" library

public class PlayerMovement : Player_Behavior
{
    private Vector2 movementDir; // Man skal kunne gemme det fra "Vector2" som kommer ind når brugeren trykker på knappen på skærmen ind på movement
    private Rigidbody2D myBody; // Her er det rigidbody man flytter rundt på
    private Animator myAnimator; // Her laver man en animator variable så den kan ændres i koden
    public Transform shakeyTransform;
    public TMP_Text label;
    [SerializeField] private float speed = 5;


    private void OnTilt(InputValue value)
    {
        var tilt = value.Get<Vector3>();
        label.text = tilt.ToString();
        Debug.Log(tilt.ToString());
    }


    private void Awake() // Denne her del - Awake - køre kun en gang når programmet starter op
    {
        myBody = GetComponent<Rigidbody2D>(); // Her sætter man myBody rigidbody til rigidbody på det gameobject man er på

        InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);
        InputSystem.EnableDevice(UnityEngine.InputSystem.GravitySensor.current);
        InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
    }
    private void Update()
    {
        Vector3 rotation = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.value;
        Vector3 rotationFixed = new Vector3(-rotation.x, -rotation.y,rotation.z);
        rotation = rotationFixed;
        shakeyTransform.rotation.ToAngleAxis(out float angle, out Vector3 axis);
        Vector3 currRotation = axis * angle;
        if ((currRotation + rotation).magnitude < 30) {
            shakeyTransform.Rotate(rotation);
        }
        if (movementDir.x != 0 || movementDir.y != 0)
        {
            isWalking = true;
            Vector3 movement = new Vector3(-movementDir.y, 0, movementDir.x) * speed;
            playerRigidBody.linearVelocity = movement;
        }
        else
        {
            isWalking = false;
        }
        
    }
    
    void OnMove(InputValue value)
    {
        movementDir = value.Get<Vector2>();
    }

}
