using Cysharp.Threading.Tasks;
using UnityEngine;

public class UpdateService : MonoBehaviour
{
    public delegate void GameEvent();

    public GameEvent OnEveryFrame;
    public GameEvent OnFixedUpdate;
    public GameEvent OnTick_005;
    public GameEvent OnTick_01;

    void Start()
    {
        Update005();
        Update01();
    }

    void Update()
    {
        OnEveryFrame?.Invoke();
    }

    void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    async UniTaskVoid Update005()
    {
        OnTick_005?.Invoke();

        await UniTask.Delay(System.TimeSpan.FromSeconds(0.05f));
        Update005();
    }

    async UniTaskVoid Update01()
    {
        OnTick_01?.Invoke();

        await UniTask.Delay(System.TimeSpan.FromSeconds(0.1f));
        Update01();
    }
}