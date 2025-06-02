using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameState_Menu : GameState
{
    public override async UniTask Enter()
    {
        base.Enter();

        ServiceLocator.Get<CursorService>().SetState(new CursorState_Show());
        Transform mainCanvas = ServiceLocator.Get<MainCanvas>().transform;

        var menu = ServiceLocator.Get<AddressablesService>().InstantiateObject(
            AllAddressableObjects.MenuUI, parent: mainCanvas, asSingle: true);
        var settings = ServiceLocator.Get<AddressablesService>().InstantiateObject(
            AllAddressableObjects.SettingsUI, parent: mainCanvas, asSingle: true);
        var quests = ServiceLocator.Get<AddressablesService>().InstantiateObject(
            AllAddressableObjects.QuestsUI, parent: mainCanvas, asSingle: true);

        await UniTask.WhenAll(menu, settings, quests);

        await ServiceLocator.Get<MenuUI>().Show();

        ServiceLocator.Get<MenuUI>().EOn_Play += LoadLastLevel;
    }

    void LoadLastLevel()
    {
        ServiceLocator.Get<MenuUI>().EOn_Play -= LoadLastLevel;
        LoadLastLevelAsync();
    }

    async UniTaskVoid LoadLastLevelAsync()
    {
        await ServiceLocator.Get<AddressablesService>().InstantiateObject(
            AllAddressableObjects.LoadingScreenUI, parent: ServiceLocator.Get<MainCanvas>().transform);
        await ServiceLocator.Get<LoadingScreenUI>().Show();

        int lastLevelIndex = ServiceLocator.Get<SaveLoadService>().CurrentSaveData.LastLevelIndex;

        if(await ServiceLocator.Get<SceneLoadinglService>().TryLoadLevelByIndex(lastLevelIndex) == false)
            await ServiceLocator.Get<SceneLoadinglService>().LoadRandomLevel();

        await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f));
        ServiceLocator.Get<GameFSM>().SetState<GameState_Gameplay>();
    }

    public override async UniTask Exit()
    {
        base.Exit();

        GameObject.Destroy(ServiceLocator.Get<MenuUI>().gameObject);
        GameObject.Destroy(ServiceLocator.Get<SettingsUI>().gameObject);
        GameObject.Destroy(ServiceLocator.Get<QuestsUI>().gameObject);
    }
}