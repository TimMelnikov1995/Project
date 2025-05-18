using Cysharp.Threading.Tasks;
using UnityEngine;

public class UI : MonoBehaviour 
{
    [SerializeField] int m_siblingIndex;

    public virtual async UniTask Show()
    {
        gameObject.transform.SetSiblingIndex(m_siblingIndex);
    }

    public virtual async UniTask Hide() { }

    public virtual void HideInstantly() { }
}