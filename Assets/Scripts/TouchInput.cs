
using UnityEngine;

public class TouchInput : InputSystem
{
    protected override bool IsInteracting()
    {
        var touches = Input.touchCount;
        return touches > 0 && Input.GetTouch(touches - 1).phase == TouchPhase.Began;
    }

    protected override Vector3 GetInteraction()
    {
        return Input.GetTouch(Input.touchCount - 1).position;
    }
}
