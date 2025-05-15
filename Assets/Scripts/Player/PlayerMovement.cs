using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// Først skal man bruge "InputSystem" library

    public class PlayerMovement : Player_Behavior
    {
        private Vector2 movementDir; // Man skal kunne gemme det fra "Vector2" som kommer ind når brugeren trykker på knappen på skærmen ind på movement
        private Animator myAnimator; // Her laver man en animator variable så den kan ændres i koden
        public Transform shakeyTransform;
        public TMP_Text label;
        [SerializeField] private float speed = 5;


        void Start()
        {
            myBody = GetComponent<Rigidbody>();
        }
        
        private void OnTilt(InputValue value)
        {
            var tilt = value.Get<Vector3>();
            label.text = tilt.ToString();
            Debug.Log(tilt.ToString());
        }


        private void Awake() // Denne her del - Awake - køre kun en gang når programmet starter op
        {
            // if (playerInput.currentControlScheme == "Touch")
            // {
            //     InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);
            //     InputSystem.EnableDevice(UnityEngine.InputSystem.GravitySensor.current);
            //     InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
            // }
        }
        private void Update()
        {
            // if (playerInput.currentControlScheme == "Touch")
            // {
            //     rotateWithGyro();
            // }
        
            if (movementDir.x != 0 || movementDir.y != 0)
            {
                isWalking = true;
                Vector3 movement = new Vector3(-movementDir.y, 0, movementDir.x) * speed;
                myBody.linearVelocity = movement;
            }
            else
            {
                isWalking = false;
                myBody.linearVelocity = Vector3.zero;
            }
        }

        private void rotateWithGyro()
        {
            Vector3 rotation = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.value;
            Vector3 rotationFixed = new Vector3(-rotation.x, -rotation.y,rotation.z);
            rotation = rotationFixed;
            shakeyTransform.rotation.ToAngleAxis(out float angle, out Vector3 axis);
            Vector3 currRotation = axis * angle;
            if ((currRotation + rotation).magnitude < 30) {
                shakeyTransform.Rotate(rotation);
            }
        }

        void OnMove(InputValue value)
        {
            Debug.Log(value.Get<Vector2>());
            movementDir = value.Get<Vector2>();
        }

    }