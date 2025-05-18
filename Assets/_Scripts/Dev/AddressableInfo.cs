using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
public class AddressableInfo : MonoBehaviour
{
    public AddressableObjectInfo[] AddressableObjectInfos;

    void Update()
    {
        UpdateInfo();
    }

    void UpdateInfo()
    {
        AddressableObjectInfos = ServiceLocator.Get<AddressablesService>().AddressableObjectInfos.ToArray();
    }
}
#endif