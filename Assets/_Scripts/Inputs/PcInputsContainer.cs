using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PcInputsContainer", menuName = "ScriptableObjects/PC Inputs Container", order = 1)]
public class PcInputsContainer : ScriptableObject
{
    [field: SerializeField] public InputActionReference LookInput { get; private set; }
    [field: SerializeField] public InputActionReference MovementInput { get; private set; }
    [field: SerializeField] public InputActionReference JumpingInput { get; private set; }
    [field: SerializeField] public InputActionReference AttackingInput { get; private set; }
    [field: SerializeField] public InputActionReference EscInput { get; private set; }
}