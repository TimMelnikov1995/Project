using UnityEngine;

public class Vfx : MonoBehaviour
{
    int _layer;

    void OnDisable()
    {
        ServiceLocator.Get<VfxService>()?.MinusVfx(_layer);
    }

    public void Init(int layer)
    {
        _layer = layer;
        ServiceLocator.Get<VfxService>()?.PlusVfx(_layer);
    }
}