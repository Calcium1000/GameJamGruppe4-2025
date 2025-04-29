using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchManager : MonoBehaviour
{
    public TMP_Text label;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnhancedTouchSupport.Enable();
        // Enable the touch simulation
        TouchSimulation.Enable();

        //add bindings to the touch events
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += Touch_onFingerDown;
    }

    private void Touch_onFingerDown(Finger finger)
    {
        //see if user pressed on an object
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(finger.screenPosition), Vector2.zero);
        if (hit)
        {
            if (hit.collider.gameObject.TryGetComponent<TouchControl>(out var touchable))
            {
                //if object implements TouchControl
                touchable.BindFinger(finger);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
