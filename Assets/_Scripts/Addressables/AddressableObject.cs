using UnityEngine;

public class AddressableObject : MonoBehaviour
{
    [field: SerializeField] public string Name;

    void OnEnable()
    {
        ServiceLocator.Get<AddressablesService>()?.ObjectEnabled(Name);
    }

    void OnDisable()
    {
        ServiceLocator.Get<AddressablesService>()?.ObjectDisabled(Name);
    }

    public void SetName(string name)
    {
        Name = name;
    }
}