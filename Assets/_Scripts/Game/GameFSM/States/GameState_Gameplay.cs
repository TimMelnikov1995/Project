using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameState_Gameplay : GameState
{
    public override async UniTask Enter()
    {
        base.Enter();

        ServiceLocator.Get<CursorService>().SetState(new CursorState_Hide());
        Transform mainCanvas = ServiceLocator.Get<MainCanvas>().transform;

        await ServiceLocator.Get<AddressablesService>().InstantiateObject(
                ProjectNames.Addressables.Player.PlayerPrefab, asSingle: true);

        var gameplayUI = ServiceLocator.Get<AddressablesService>().InstantiateObject(
            ProjectNames.Addressables.UI.GameplayUI, parent: mainCanvas, asSingle: true);
        var pauseUI = ServiceLocator.Get<AddressablesService>().InstantiateObject(
            ProjectNames.Addressables.UI.PauseUI, parent: mainCanvas, asSingle: true);

        await UniTask.WhenAll(gameplayUI, pauseUI);

        if (ServiceLocator.TryGet(out LoadingScreenUI loadingScreenUI))
        {
            await loadingScreenUI.Hide();
            GameObject.Destroy(loadingScreenUI.gameObject);
        }

        await ServiceLocator.Get<GameplayUI>().Show();
        ServiceLocator.Get<InputService>().SetActive(true);
        ServiceLocator.Get<TimeService>().SetTimeScale(1);

        ServiceLocator.Get<InputService>().EOn_Esc += Pause;
        ServiceLocator.Get<EventBus>().Subscribe<Event_Finished>(Finished);
    }

    void Pause()
    {
        Debug.Log("Pause");
        ServiceLocator.Get<InputService>().EOn_Esc -= Pause;
        ServiceLocator.Get<GameFSM>().SetState<GameState_Pause>();
    }

    void Finished(Event_Finished finished)
    {
        ServiceLocator.Get<EventBus>().Unsubscribe<Event_Finished>(Finished);

        // Go to finish state.
    }

    public override async UniTask Exit()
    {
        base.Exit();
    }
}