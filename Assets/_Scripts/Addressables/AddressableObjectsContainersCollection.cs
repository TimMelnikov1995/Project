using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddressableObjectsContainersCollection", menuName = "ScriptableObjects/Addressable Objects Containers Collection", order = 1)]
public class AddressableObjectsContainersCollection : ScriptableObject
{
    [SerializeField] List<AddressableObjectsContainer> m_addressableObjectsContainersCollection;
    [field: SerializeField, HideInInspector] public List<AddressableObjectInfo> AddressableObjectInfos = new();

#if UNITY_EDITOR
    void OnValidate()
    {
        AddressableObjectInfos.Clear();

        foreach (var container in m_addressableObjectsContainersCollection)
            foreach (var objectInfo in container.AddressableObjectInfos)
                AddressableObjectInfos.Add(objectInfo);
    }
#endif
}