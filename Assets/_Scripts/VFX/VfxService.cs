using System.Collections.Generic;
using UnityEngine;

public class VfxService
{
    VfxSettings _settings;
    Dictionary<string, Vfx> _allVfx = new();
    Dictionary<int, int> _currentVfxOnLayer = new();

    public VfxService()
    {
        _settings = Resources.Load<VfxSettings>("VFX/VfxSettings");
        _allVfx = Resources.Load<VfxPoolCollection>("VFX/VfxPoolCollection").VFX;
    }

    public void Spawn(string name, Vector3 position = default, Quaternion rotation = default, Transform parent = null, int layer = 0)
    {
        if (_currentVfxOnLayer.ContainsKey(layer) == false)
            _currentVfxOnLayer.Add(layer, 0);

        if (_currentVfxOnLayer.TryGetValue(layer, out int count))
            if (count >= _settings.MaximumVfxPerLayer)
                return;

        if (_allVfx.TryGetValue(name, out Vfx vfx))
        {
            GameObject.Instantiate(vfx, position, rotation, parent).Init(layer);
        }
    }

    public void PlusVfx(int layer)
    {
        if (_currentVfxOnLayer.ContainsKey(layer) == false) 
            _currentVfxOnLayer.Add(layer, 0);

        _currentVfxOnLayer[layer]++;
    }

    public void MinusVfx(int layer) 
    {
        if (_currentVfxOnLayer.ContainsKey(layer) == false)
            _currentVfxOnLayer.Add(layer, 0);

        _currentVfxOnLayer[layer]--;
    }
}