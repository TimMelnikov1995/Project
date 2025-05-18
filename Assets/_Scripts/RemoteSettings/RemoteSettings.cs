using UnityEngine;

[CreateAssetMenu(fileName = "RemoteSettings", menuName = "ScriptableObjects/RemoteSettings", order = 1)]
public class RemoteSettings : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public string GridId { get; private set; }
    [field: SerializeField] public bool LoadAtRuntime { get; private set; }
    [Space]



    #region
    public float PlayerMovementSpeed;
    public float Sensitivity;
    public float PlayerGravity;
    public float PlayerJumpHeigh;
    #endregion



    [ContextMenu("OpenURL")]
    void OpenURL()
    {
        string url = $@"https://docs.google.com/spreadsheets/d/{Id}/edit?gid={GridId}#gid={GridId}";
        Application.OpenURL(url);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        RemoteSettingsAPI.LoadRemoteSettings(this);
    }
}