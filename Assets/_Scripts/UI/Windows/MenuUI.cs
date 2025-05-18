using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : FadableUI
{
    [SerializeField] Button m_playButton;
    [SerializeField] Button m_settingsButton;

    public event Action EOn_Play;

    void OnEnable()
    {
        ServiceLocator.Register(this);

        m_playButton.onClick.AddListener(PlayButtonClicked);
        m_settingsButton.onClick.AddListener(SettingsButtonClicked);
    }

    void OnDisable()
    {
        ServiceLocator.Unregister(this);

        m_playButton.onClick.RemoveListener(PlayButtonClicked);
        m_settingsButton.onClick.RemoveListener(SettingsButtonClicked);
    }

    void PlayButtonClicked()
    {
        EOn_Play?.Invoke();
    }

    void SettingsButtonClicked()
    {
        ServiceLocator.Get<SettingsUI>()?.Show();
    }
}