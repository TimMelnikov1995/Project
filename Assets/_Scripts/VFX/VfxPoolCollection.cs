using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VfxPoolCollection", menuName = "ScriptableObjects/Vfx Pool Collection", order = 1)]
public class VfxPoolCollection : ScriptableObject
{
    [SerializeField] VfxPool[] m_vfxPools;

    [field: SerializeField] public Dictionary<string, Vfx> VFX { get; private set; } = new();

#if UNITY_EDITOR
    void OnValidate()
    {
        VFX.Clear();

        foreach (var pool in m_vfxPools)
            foreach (var vfx in pool.Vfx)
                VFX.TryAdd(vfx.Name, vfx.Object);
    }
#endif
}