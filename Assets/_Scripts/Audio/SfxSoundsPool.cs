using UnityEngine;

[CreateAssetMenu(fileName = "SfxSoundsPool", menuName = "ScriptableObjects/SfxSoundsPool", order = 1)]
public partial class SfxSoundsPool : ScriptableObject
{
    [field: SerializeField] public Sound[] Sounds;
}