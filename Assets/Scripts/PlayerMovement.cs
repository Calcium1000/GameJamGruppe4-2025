using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
// Først skal man bruge "InputSystem" library

public class PlayerMovement : MonoBehaviour
{

    private Vector2 movement; // Man skal kunne gemme det fra "Vector2" som kommer ind når brugeren trykker på knappen på skærmen ind på movement
    private Rigidbody2D myBody; // Her er det rigidbody man flytter rundt på
    private Animator myAnimator; // Her laver man en animator variable så den kan ændres i koden
    public Transform shakeyTransform;
    public TMP_Text label;

    private void OnTilt(InputValue value)
    {
        var tilt = value.Get<Vector3>();
        label.text = tilt.ToString();

    }

    [SerializeField] private int speed = 5;

    private void Awake() // Denne her del - Awake - køre kun en gang når programmet starter op

    // Start is called before the first frame update
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
        label.text = "val : " + rotation.ToString();
        shakeyTransform.rotation.ToAngleAxis(out float angle, out Vector3 axis);
        Vector3 currRotation = axis * angle;
        if ((currRotation + rotation).magnitude < 30) {
            shakeyTransform.Rotate(rotation);
        }

        
    }
    // Update is called once per frame
    private void OnMovement(InputValue value) // Her laver man en funktion som holder øje med Input systems value, altså dens værdi
    {
        movement = value.Get<Vector2>(); // Her bliver movement sat til Vector2 fra Input Action når brugeren trykker på WASD-knapperne

        if (movement.x != 0 || movement.y != 0) { // Her bliver value.vector2 sat til [0,0] når WASD ikke bliver trykket på

        }
        else 
        {

        }
}

    [System.Obsolete]
    private void FixedUpdate() // Her giver man velocity af rigidbody2D den hastighed som er blevet sat
    {
        myBody.linearVelocity = movement * speed;
    }
}
