using System;
using UnityEngine;

public abstract class InputService
{
    public abstract event Action<Vector2> EOn_LookInput;
    public abstract event Action<Vector2> EOn_MovementInput;
    public abstract event Action<bool> EOn_JumpingInput;
    public abstract event Action<bool> EOn_AttackingInput;
    public abstract event Action EOn_Esc;

    public abstract void SetActive(bool active);
}