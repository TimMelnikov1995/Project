using UnityEngine;

public class CursorState_Hide : CursorState
{
    protected override void Enter()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}