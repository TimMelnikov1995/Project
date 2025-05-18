using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            ServiceLocator.Get<EventBus>().CallEvent(new Event_Finished());
    }
}