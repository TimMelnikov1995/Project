using System.Collections.Generic;
using UnityEngine;
using static SfxSoundsPool;

[CreateAssetMenu(fileName = "SfxSoundsPoolsCollection", menuName = "ScriptableObjects/Sfx Sounds Pools Collection", order = 1)]
public class SfxSoundsPoolsCollection : ScriptableObject
{
    [SerializeField] SfxSoundsPool[] m_soundPools;
    [field: SerializeField] public Dictionary<string, Sound> Sounds { get; private set; } = new();

#if UNITY_EDITOR
    void OnValidate()
    {
        Sounds.Clear();

        foreach (var pool in m_soundPools)
            foreach (var sound in pool.Sounds)
                Sounds.TryAdd(sound.Name, sound);
    }
#endif
}