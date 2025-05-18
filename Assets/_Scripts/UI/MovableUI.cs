using Cysharp.Threading.Tasks;

public class MovableUI : FadableUI
{
    public override async UniTask Show()
    {
        base.Show();
    }

    public override async UniTask Hide()
    {
        base.Hide();
    }

    public override void HideInstantly()
    {
        base.HideInstantly();
    }
}