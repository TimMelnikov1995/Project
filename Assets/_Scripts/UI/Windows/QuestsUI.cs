public class QuestsUI : FadableUI
{
    void OnEnable()
    {
        ServiceLocator.Register(this);
    }

    void OnDisable()
    {
        ServiceLocator.Unregister(this);
    }
}