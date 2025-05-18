using UnityEngine;

public class CursorState_Show : CursorState
{
    protected override void Enter()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
