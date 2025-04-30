using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Først skal man bruge "InputSystem" library

public class PlayerMovement : MonoBehaviour
{

    private Vector2 movement; // Man skal kunne gemme det fra "Vector2" som kommer ind når brugeren trykker på knappen på skærmen ind på movement
    private Rigidbody2D myBody; // Her er det rigidbody man flytter rundt på
    private Animator myAnimator; // Her laver man en animator variable så den kan ændres i koden

    [SerializeField] private int speed = 5;

    private void Awake() // Denne her del - Awake - køre kun en gang når programmet starter op

    // Start is called before the first frame update
    {
        myBody = GetComponent<Rigidbody2D>(); // Her sætter man myBody rigidbody til rigidbody på det gameobject man er på
        myAnimator = GetComponent<Animator>(); // Man vil gerne kunne "lege" med den animator som sidder på ens gameobject
    }

    // Update is called once per frame
    private void OnMovement(InputValue value) // Her laver man en funktion som holder øje med Input systems value, altså dens værdi
    {
        movement = value.Get<Vector2>(); // Her bliver movement sat til Vector2 fra Input Action når brugeren trykker på WASD-knapperne

        if (movement.x != 0 || movement.y != 0) { // Her bliver value.vector2 sat til [0,0] når WASD ikke bliver trykket på
                                                  // spilleren kigger samtidig hele tiden op når man ikke længere trykker. For at dette ikke
                                                  // skal ske, kan man ændre på animation, men kun hvis mindst en af x eller y ikke er = 0.
        myAnimator.SetFloat("x", movement.x);     // Her sætter man x ind i animator til movement.x som kommer fra unity input
        myAnimator.SetFloat("y", movement.y);     // Her sætter man så y ind i animator til movement.y som også kommer fra unity input

            myAnimator.SetBool("isWalking", true); // Hvis movement.x eller y ikke er = 0, betyder det at spriten går
        }
    else 
    {
        myAnimator.SetBool("isWalking", false);    // Ellers går spriten ikke, så her sættes den til at være false
    }
}

    [System.Obsolete]
    private void FixedUpdate() // Her giver man velocity af rigidbody2D den hastighed som er blevet sat
    {
        myBody.velocity = movement * speed;
    }
}
