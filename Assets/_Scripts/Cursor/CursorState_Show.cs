using UnityEngine;

public class CursorState_Show : CursorState
{
    public override void Enter()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
