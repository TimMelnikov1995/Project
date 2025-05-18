using UnityEngine;

public class LoadAddressableObjectsTrigger : MonoBehaviour
{
    [SerializeField] string[] m_objectsNames;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            foreach (string name in m_objectsNames)
                ServiceLocator.Get<AddressablesService>()?.Load(name);
    }
}