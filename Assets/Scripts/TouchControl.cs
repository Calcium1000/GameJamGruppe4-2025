using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public abstract class TouchControl : MonoBehaviour
{
    public Finger finger { get; protected set; }

    public void BindFinger(Finger finger)
    {
        this.finger = finger;
        OnPress();
    }

    protected virtual void OnPress()
    {

    }

    protected virtual void OnRelease()
    {

    }
    private void Update()
    {
        CheckActive();
    }
    protected virtual void OnReleaseOther()
    {
    }
    private void CheckActive()
    {
        if (finger == null)
        {
            return;
        }
        if (!finger.isActive)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(finger.screenPosition), Vector2.zero);
            //see if it was released on itself
            if (hit) {
                if (hit.collider.gameObject == this.gameObject)
                {
                    OnRelease();
                }
                else
                {
                    //if the finger is on another object
                    OnReleaseOther();
                }
            } else
            {
                //if the finger hit nothing
                OnReleaseOther();
            }
            finger = null;
            return;

        } else
        {
            TUpdate();
        }
    }
    /// <summary>
    /// This is called each frame when the finger is active
    /// </summary>
    protected virtual void TUpdate()
    {
        
    }
}
