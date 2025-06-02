using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableObject : MonoBehaviour
{
    [SerializeField] AddressableObjectsContainer m_container;
    [field: SerializeField] public string Name { get; private set; }
    public AssetReferenceGameObject AssetReference { get; private set; }

    void OnEnable()
    {
        ServiceLocator.Get<AddressablesService>()?.ObjectEnabled(Name.ToString());
    }

    void OnDisable()
    {
        ServiceLocator.Get<AddressablesService>()?.ObjectDisabled(Name.ToString());
    }

    [ContextMenu("Add This Object To Addressables")]
    void AddThisObjectToAddressables()
    {
        if (m_container == null)
            return;

        EnableAddressableFlag();
        AddressableObjectsEnumGenerator.Add(name);
        Name = name;
        m_container.Add(this);
    }

    void EnableAddressableFlag()
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        string assetGUID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this));
        settings.CreateAssetReference(assetGUID);
        AssetReference = new AssetReferenceGameObject(assetGUID);
    }
}