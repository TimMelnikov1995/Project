using UnityEngine;

[CreateAssetMenu(fileName = "VfxPool", menuName = "ScriptableObjects/Vfx Pool", order = 1)]
public class VfxPool : ScriptableObject
{
    [field: SerializeField] public VfxInfo[] Vfx { get; private set; }
}
