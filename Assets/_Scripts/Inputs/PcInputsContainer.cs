using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PcInputsContainer", menuName = "ScriptableObjects/PC Inputs Container", order = 1)]
public class PcInputsContainer : ScriptableObject
{
    [field: SerializeField] public InputActionReference LookInput;
    [field: SerializeField] public InputActionReference MovementInput;
    [field: SerializeField] public InputActionReference JumpingInput;
    [field: SerializeField] public InputActionReference AttackingInput;
    [field: SerializeField] public InputActionReference EscInput;
}