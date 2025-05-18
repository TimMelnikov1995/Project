using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadableUI : UI
{
    [SerializeField] float m_fadeInTime = 0.5f;
    [SerializeField] float m_fadeOutTime = 0.5f;

    CanvasGroup _canvasGroup;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        transform.localPosition = Vector3.zero;
        HideInstantly();
    }

    public override async UniTask Show()
    {
        base.Show();

        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(1, m_fadeInTime).SetUpdate(true);

        await UniTask.Delay(System.TimeSpan.FromSeconds(m_fadeInTime), ignoreTimeScale: true);
    }

    public override async UniTask Hide()
    {
        base.Hide();

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0, m_fadeOutTime).SetUpdate(true);

        await UniTask.Delay(System.TimeSpan.FromSeconds(m_fadeInTime), ignoreTimeScale: true);
    }

    public override void HideInstantly()
    {
        base.HideInstantly();

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0;
    }
}