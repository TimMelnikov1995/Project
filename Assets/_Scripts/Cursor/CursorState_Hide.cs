using UnityEngine;

public class CursorState_Hide : CursorState
{
    public override void Enter()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}