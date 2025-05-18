using UnityEngine;

public class Player : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        ServiceLocator.Register(this, typeof(Player));
    }

    protected virtual void OnDisable()
    {
        ServiceLocator.Unregister(this, typeof(Player));
    }
}