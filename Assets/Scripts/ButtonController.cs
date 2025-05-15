using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ButtonController : TouchControl
{
    protected override void OnRelease()
    {
        Debug.Log("guh");
    }
}