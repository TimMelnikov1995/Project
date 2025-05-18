using UnityEngine;

[CreateAssetMenu(fileName = "LevelsPool", menuName = "ScriptableObjects/Levels Pool", order = 1)]
public partial class LevelsPool : ScriptableObject
{
    [field: SerializeField] public LevelInfo[] Levels;
}