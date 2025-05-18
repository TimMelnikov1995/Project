using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameState
{
    public virtual async UniTask Enter()
    {
        Debug.Log("Enter " + this.GetType().ToString());
    }

    public virtual async UniTask Exit()
    {
        Debug.Log("Exit " + this.GetType().ToString());
    }
}