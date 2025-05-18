using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : FadableUI
{
    [SerializeField] Button m_continueButton;

    public event Action EOn_Continue;

    void OnEnable()
    {
        ServiceLocator.Register(this);
        m_continueButton.onClick.AddListener(Continue);
    }

    void OnDisable()
    {
        ServiceLocator.Unregister(this);
        m_continueButton.onClick.RemoveListener(Continue);
    }

    void Continue()
    {
        EOn_Continue?.Invoke();
    }
}