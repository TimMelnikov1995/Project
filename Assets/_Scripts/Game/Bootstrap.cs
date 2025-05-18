using IGM.Localization;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] MainCanvas m_mainCanvas;

    void Start()
    {
        CreateServiceLocator();
        CreateRemoteSettings();

        ServiceLocator.Register(new GameFSM());
        ServiceLocator.Register(new SaveLoadService());
        ServiceLocator.Register(new AddressablesService());
        ServiceLocator.Register(new TimeService());
        ServiceLocator.Register(new SceneLoadinglService());
        ServiceLocator.Register(new AudioService());
        ServiceLocator.Register(new VfxService());
        ServiceLocator.Register(new EventBus());
        ServiceLocator.Register(new CursorService());

        CreateLocalizationService();
        CreateInputService();
        CreateUpdateService();
        CreateMainCanvas();

        ServiceLocator.Get<SaveLoadService>().EOn_Load += OnLoadSavingsComplete;
        ServiceLocator.Get<SaveLoadService>().Load();
    }



    void CreateServiceLocator()
    {
        DontDestroyOnLoad(new GameObject("ServiceLocator").AddComponent<ServiceLocator>());
    }

    void CreateUpdateService()
    {
        UpdateService updateService = new GameObject("UpdateService").AddComponent<UpdateService>();
        DontDestroyOnLoad(updateService);
        ServiceLocator.Register(updateService);
    }

    void CreateMainCanvas()
    {
        MainCanvas mainCanvas = Instantiate(m_mainCanvas);
        DontDestroyOnLoad(mainCanvas);
        ServiceLocator.Register(mainCanvas);
    }

    void CreateInputService()
    {
        InputService inputService = new PcInputService(); // Replace depending on platform.
        ServiceLocator.Register(inputService, typeof(InputService));
    }

    void CreateLocalizationService()
    {
        LocalizationService localizationService = new LocalizationService();
        localizationService.SetLanguage("en"); // Set depending on the device language
        ServiceLocator.Register(localizationService);
    }

    void CreateRemoteSettings()
    {
        RemoteSettings remoteSettings = Resources.Load<RemoteSettings>("RemoteSettings");
        if (remoteSettings.LoadAtRuntime)
            remoteSettings.Load();
        ServiceLocator.Register(remoteSettings);
    }



    void OnLoadSavingsComplete()
    {
        ServiceLocator.Get<SaveLoadService>().EOn_Load -= OnLoadSavingsComplete;

        // Set language from savings.

        ServiceLocator.Get<GameFSM>().SetState<GameState_Menu>();
        Destroy(gameObject);
    }
}