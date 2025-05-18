using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesService
{
    Dictionary<string, AddressableObjectInfo> _addressableObjectInfos = new();
    public IEnumerable<AddressableObjectInfo> AddressableObjectInfos => _addressableObjectInfos.Values;

    public AddressablesService()
    {
        AddressableObjectInfo[] addressableObjectInfos = 
            Resources.Load<AddressableObjectsContainersCollection>("AddressableObjectsContainersCollection").AddressableObjectInfos.ToArray();

        _addressableObjectInfos.Clear();
        foreach (AddressableObjectInfo objectInfo in addressableObjectInfos) 
        {
            _addressableObjectInfos.Add(objectInfo.Name, objectInfo);
            objectInfo.Init(objectInfo.Name);
        }
            
    }

    public async UniTask<AsyncOperationHandle<GameObject>> Load(string name)
    {
        if (_addressableObjectInfos.TryGetValue(name, out AddressableObjectInfo objectInfo))
        {
            if (objectInfo.IsLoaded == false)
            {
                objectInfo.OpHandle = objectInfo.Object.LoadAssetAsync();

                await objectInfo.OpHandle;
                objectInfo.SetIsLoaded(true);
                return objectInfo.OpHandle;
            }
            else
            {
                return objectInfo.OpHandle;
            }
        }

        Debug.LogError("Cant find addressable object " + name);
        return new();
    }

    public async UniTask<GameObject> InstantiateObject(string name, Vector3 position = default, Quaternion rotation = default, Transform parent = null, bool asSingle = false)
    {
        if (asSingle)
            if (_addressableObjectInfos.TryGetValue(name, out AddressableObjectInfo objectInfo))
                if (objectInfo.InstancesCount != 0)
                {
                    return null;
                }   

        AsyncOperationHandle<GameObject> opHandle = await Load(name);

        if (opHandle.IsValid() == false)
        {
            Debug.LogError($"Instantiating object {name} failed");
            return null;
        }   

        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            return GameObject.Instantiate(opHandle.Result, position, rotation, parent).gameObject;
        }
        else
        {
            Debug.LogError($"Instantiating object {name} failed");
            return null;
        }
    }

    public void ObjectEnabled(string name)
    {
        if (_addressableObjectInfos.TryGetValue(name, out AddressableObjectInfo objectInfo))
        {
            objectInfo.PlusInstance();
        }
    }

    public void ObjectDisabled(string name)
    {
        if (_addressableObjectInfos.TryGetValue(name, out AddressableObjectInfo objectInfo))
        {
            objectInfo.MinusInstance();
            if (objectInfo.InstancesCount == 0)
            {
                objectInfo.OpHandle.Release();
                objectInfo.SetIsLoaded(false);
            }
        }
    }
}

[Serializable]
public class AddressableObjectInfo
{
    [field: SerializeField] public string Name;
    [field: SerializeField] public AssetReferenceGameObject Object;

    public int InstancesCount { get; private set; }
    public bool IsLoaded { get; private set; }
    public AsyncOperationHandle<GameObject> OpHandle { get; set; }

    public void Init(string name)
    {
        OpHandle = new();
        Name = name;
    }

    public void PlusInstance()
    {
        InstancesCount++;
    }

    public void MinusInstance()
    {
        InstancesCount--;
        if (InstancesCount < 0)
            InstancesCount = 0;
    }

    public void SetIsLoaded(bool isLoaded)
    {
        IsLoaded = isLoaded;
    }
}