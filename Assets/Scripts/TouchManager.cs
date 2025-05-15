using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering.Universal;

public class TouchManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Camera UICamera;

    private void Start()
    {
        UICamera = Camera.main.GetComponent<UniversalAdditionalCameraData>().cameraStack[0];
        EnhancedTouchSupport.Enable();
        // Enable the touch simulation
        TouchSimulation.Enable();

        //add bindings to the touch events
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += Touch_onFingerDown;
    }

    private void Touch_onFingerDown(Finger finger)
    {
        //see if user pressed on an object
        var hit = Physics2D.Raycast(UICamera.ScreenToWorldPoint(finger.screenPosition), Vector2.zero);
        if (hit)
            if (hit.collider.gameObject.TryGetComponent<TouchControl>(out var touchable))
                //if object implements TouchControl
                touchable.BindFinger(finger);
    }


    // Update is called once per frame
    private void Update()
    {
    }
}