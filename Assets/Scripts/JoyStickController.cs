using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering.Universal;

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
        // Make local variables for reuse
        RectTransform _parentRectTransform = ParentRectTransform;
        Vector3 _parentPosition = ParentPosition;
        Camera UICamera = Camera.main.GetComponent<UniversalAdditionalCameraData>().cameraStack[0];
        // Get the depth of the parent object in the 3D world
        float parentDepth = UICamera.WorldToScreenPoint(_parentPosition).z;

        // Convert the finger's screen position to world position, using the parent's depth
        Vector3 fingerPosRelative = UICamera.ScreenToWorldPoint(new Vector3(finger.screenPosition.x, finger.screenPosition.y, parentDepth));

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

        // Set the value of the joystick (normalized to a range of -1 to 1)
        JoyStickValue = new Vector2(direction.x / radius, direction.y / radius);

        // Update the knob's position
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
