using UnityEngine;

[CreateAssetMenu(fileName = "VfxSettings", menuName = "ScriptableObjects/Vfx Settings", order = 1)]
public class VfxSettings : ScriptableObject
{
    [field: SerializeField] public int MaximumVfxPerLayer { get; private set; } = 100;
}