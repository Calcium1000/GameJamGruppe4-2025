using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class JoyStickController : TouchControl
{

    public GameObject knob;
    public Vector2 JoyStickValue { get; private set; }
    RectTransform ParentRectTransform => knob.transform.parent.GetComponent<RectTransform>();
    Vector3 ParentPosition => ParentRectTransform.position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void TUpdate()
    {

        //make local variables for reuse
        RectTransform _parentRectTransform = ParentRectTransform;
        Vector3 _parentPosition = ParentPosition;
        //unbind finger if it is no longer on the screen
        if (!finger.isActive)
        {
            //finger released
            finger = null;
            knob.transform.position = _parentPosition;
            return;
        }
        

            Vector3 fingerPosRelative = Camera.main.ScreenToWorldPoint(finger.screenPosition);
            fingerPosRelative.z = 0;

            // Calculate the radius based on the RectTransform's size and scale
            float radius = (_parentRectTransform.rect.width * 0.5f) * _parentRectTransform.lossyScale.x;

            // Calculate the direction and distance from the parent to the finger position
            Vector3 direction = fingerPosRelative - _parentPosition;
            float distance = direction.magnitude;

            // Clamp the position to the circle's radius
            if (distance > radius)
            {
                direction = direction.normalized; // Normalize the direction vector
                fingerPosRelative = _parentPosition + direction * radius;
            }
            //set the value of the joystick
            JoyStickValue = direction * radius;

            knob.transform.position = fingerPosRelative;

    }

    protected override void OnReleaseOther()
    {
        ResetKnob();
    }
    protected override void OnRelease()
    {
        ResetKnob();
    }
    private void ResetKnob()
    {
        //finger released
        finger = null;
        knob.transform.position = ParentPosition;
    }
}
