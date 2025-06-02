using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Addressable Objects Container", menuName = "ScriptableObjects/Addressable Objects Container", order = 1)]
public class AddressableObjectsContainer : ScriptableObject
{
    [field: SerializeField] public List<AddressableObjectInfo> AddressableObjectInfos { get; private set; }

#if UNITY_EDITOR
    void OnValidate()
    {
        foreach (var objectInfo in AddressableObjectInfos)
        {
            objectInfo.Name = objectInfo.Object.editorAsset.gameObject.GetComponent<AddressableObject>().Name;
        }
    }

    public void Add(AddressableObject newGo)
    {
        AddressableObjectInfo objectInfo = new AddressableObjectInfo();
        objectInfo.Name = newGo.Name;
        objectInfo.Object = newGo.AssetReference;

        AddressableObjectInfos.Add(objectInfo);
    }
#endif
}