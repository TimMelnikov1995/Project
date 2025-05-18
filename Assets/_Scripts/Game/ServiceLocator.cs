using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    #region Singelton
    static ServiceLocator _instance;
    static ServiceLocator Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<ServiceLocator>();

            return _instance;
        }
    }
    #endregion

    readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
    public IEnumerable<object> RegisteredServices => _services.Values;

    public static T Get<T>() where T : class
    {
        if (!Instance)
            return null;
        
        Type type = typeof(T); 
        if (Instance._services.TryGetValue(type, out object obj))
        {
            return obj as T;
        }

        Debug.LogError($"Service of type '{type.FullName}' not registered");
        return null;
    }

    public static bool TryGet<T>(out T service) where T : class
    {
        if (!Instance)
        {
            service = null;
            return false;
        }   

        Type type = typeof(T);
        if (Instance._services.TryGetValue(type, out object obj))
        {
            service = obj as T;
            return true;
        }

        service = null;
        return false;
    }

    public static void Register(object service, Type specialType = null)
    {
        if (!Instance)
            return;

        Type type = service.GetType();
        if (specialType != null)
            type = specialType;

        if (Instance._services.TryAdd(type, service) == false)
        {
            Debug.LogError($"Service of type '{type.FullName}' already registered");
        }

        Debug.Log("Register " + type.ToString());
    }

    public static void Unregister(object service)
    {
        if (!Instance)
            return;

        Type type = service.GetType();
        if (Instance._services.Remove(type) == false)
        {
            Debug.Log($"Cant unregister service of type '{type.FullName}', it is not registered");
        }

        Debug.Log("Unregister " + type.ToString());
    }
}