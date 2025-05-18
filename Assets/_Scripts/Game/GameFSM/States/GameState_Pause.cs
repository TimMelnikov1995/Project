using Cysharp.Threading.Tasks;

public class GameState_Pause : GameState
{
    public override async UniTask Enter()
    {
        base.Enter();

        ServiceLocator.Get<CursorService>().SetState(new CursorState_Show());
        ServiceLocator.Get<InputService>().SetActive(false);
        ServiceLocator.Get<TimeService>().SetTimeScale(0);

        await ServiceLocator.Get<PauseUI>().Show();
        ServiceLocator.Get<PauseUI>().EOn_Continue += Continue;
        ServiceLocator.Get<InputService>().EOn_Esc += Continue;
    }

    void Continue()
    {
        ServiceLocator.Get<PauseUI>().EOn_Continue -= Continue;
        ServiceLocator.Get<InputService>().EOn_Esc -= Continue;
        ServiceLocator.Get<GameFSM>().SetState<GameState_Gameplay>();
    }

    public override async UniTask Exit()
    {
        base.Exit();

        await ServiceLocator.Get<PauseUI>().Hide();
    }
}