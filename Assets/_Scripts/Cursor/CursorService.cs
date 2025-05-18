public class CursorService
{
    protected virtual void Enter() { }

    public void SetState(CursorState cursorState)
    {
        cursorState.Enter();
    }
}