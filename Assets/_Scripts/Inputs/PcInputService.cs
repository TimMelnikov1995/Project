using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PcInputService : InputService
{
    public override event Action<Vector2> EOn_LookInput;
    public override event Action<Vector2> EOn_MovementInput;
    public override event Action<bool> EOn_JumpingInput;
    public override event Action<bool> EOn_AttackingInput;
    public override event Action EOn_Esc;

    bool _isActive;

    public PcInputService()
    {
        PcInputsContainer pcInputsContainer =
            Resources.Load<PcInputsContainer>("PcInputsContainer");

        pcInputsContainer.LookInput.ToInputAction().performed += LookInputPerformed;
        pcInputsContainer.LookInput.ToInputAction().canceled += LookInputCanceled;

        pcInputsContainer.MovementInput.ToInputAction().performed += MovementInputPerformed;
        pcInputsContainer.MovementInput.ToInputAction().canceled += MovementInputCanceled;

        pcInputsContainer.JumpingInput.ToInputAction().performed += JumpingInputPerformed;
        pcInputsContainer.JumpingInput.ToInputAction().canceled += JumpingInputCanceled;

        pcInputsContainer.AttackingInput.ToInputAction().performed += AttackingInputPerformed;
        pcInputsContainer.AttackingInput.ToInputAction().canceled += AttackingInputCanceled;

        pcInputsContainer.EscInput.ToInputAction().performed += EscInput;
    }

    #region Look
    void LookInputPerformed(InputAction.CallbackContext context)
    {
        if (_isActive == false)
            return;

        EOn_LookInput?.Invoke(context.ReadValue<Vector2>());
    }

    void LookInputCanceled(InputAction.CallbackContext context = default)
    {
        EOn_LookInput?.Invoke(Vector2.zero);
    }
    #endregion

    #region Movement
    void MovementInputPerformed(InputAction.CallbackContext context)
    {
        if (_isActive == false)
            return;

        EOn_MovementInput?.Invoke(context.ReadValue<Vector2>());
    }

    void MovementInputCanceled(InputAction.CallbackContext context = default)
    {
        EOn_MovementInput?.Invoke(Vector2.zero);
    }
    #endregion

    #region Jumping
    void JumpingInputPerformed(InputAction.CallbackContext context)
    {
        if (_isActive == false)
            return;

        EOn_JumpingInput?.Invoke(true);
    }

    void JumpingInputCanceled(InputAction.CallbackContext context = default)
    {
        EOn_JumpingInput?.Invoke(false);
    }
    #endregion

    #region Attacking
    void AttackingInputPerformed(InputAction.CallbackContext context)
    {
        if (_isActive == false)
            return;

        EOn_AttackingInput?.Invoke(true);
    }

    void AttackingInputCanceled(InputAction.CallbackContext context = default)
    {
        EOn_AttackingInput?.Invoke(false);
    }
    #endregion

    void EscInput(InputAction.CallbackContext context = default)
    {
        EOn_Esc?.Invoke();
    }



    public override void SetActive(bool active)
    {
        _isActive = active;

        if (_isActive == true)
            return;

        EOn_MovementInput?.Invoke(Vector2.zero);
        EOn_JumpingInput?.Invoke(false);
        EOn_AttackingInput?.Invoke(false);
    }
}