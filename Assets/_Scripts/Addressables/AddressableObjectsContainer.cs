using UnityEngine;

[CreateAssetMenu(fileName = "Addressable Objects Container", menuName = "ScriptableObjects/Addressable Objects Container", order = 1)]
public class AddressableObjectsContainer : ScriptableObject
{
    [field: SerializeField] public AddressableObjectInfo[] AddressableObjectInfos { get; private set; }

#if UNITY_EDITOR
    void OnValidate()
    {
        foreach (var objectInfo in AddressableObjectInfos)
        {
            objectInfo.Object.editorAsset.gameObject.GetComponent<AddressableObject>().SetName(objectInfo.Name);
        }
    }
#endif
}