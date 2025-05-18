public class SettingsUI : MovableUI
{

    void OnEnable()
    {
        ServiceLocator.Register(this);
    }

    void OnDisable()
    {
        ServiceLocator.Unregister(this);
    }

    public void Close()
    {
        Hide();
    }
}