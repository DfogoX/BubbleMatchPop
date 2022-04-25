
using UnityEngine;

public class MouseInput : InputSystem
{
    protected override bool IsInteracting()
    {
        return Input.GetMouseButtonDown(0);
    }

    protected override Vector3 GetInteraction()
    {
        return Input.mousePosition;
    }
}
