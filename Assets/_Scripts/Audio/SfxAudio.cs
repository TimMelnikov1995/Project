using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SfxAudio
{
    List<AudioSource> _freeImportantSfxSources = new();
    List<AudioSource> _busyImportantSfxSources = new();
    List<AudioSource> _freeUnimportantSfxSources = new();
    List<AudioSource> _busyUnimportantSfxSources = new();

    Dictionary<int, UnimportantLayer> _unimportantLayers = new();

    SfxAudioSettings _sfxAudioSettings;
    GameObject _audioParent;



    public SfxAudio()
    {
        _audioParent = new GameObject("AudioParent");
        GameObject.DontDestroyOnLoad(_audioParent);

        _sfxAudioSettings = Resources.Load<SfxAudioSettings>("Audio/SfxAudioSettings");

        CreateAudioSources();
    }



    void CreateAudioSources()
    {
        for (int i = 0; i < _sfxAudioSettings.ImportantSfxCount; i++)
        {
            AudioSource newSource = GameObject.Instantiate(_sfxAudioSettings.ImportantSfxSourcePrefab, _audioParent.transform);
            _freeImportantSfxSources.Add(newSource);
        }

        for (int i = 0; i < _sfxAudioSettings.UnimportanSfxtCount; i++)
        {
            AudioSource newSource = GameObject.Instantiate(_sfxAudioSettings.UnimportantSfxSourcePrefab, _audioParent.transform);
            _freeUnimportantSfxSources.Add(newSource);
        }
    }

    async UniTaskVoid UnimportantSfxDelay(int layer)
    {
        if (_unimportantLayers.ContainsKey(layer) == false)
            _unimportantLayers.Add(layer, new UnimportantLayer());

        if (_unimportantLayers.TryGetValue(layer, out UnimportantLayer unimportantLayer))
        {
            unimportantLayer.IsBusy = true;

            await UniTask.Delay(TimeSpan.FromSeconds(_sfxAudioSettings.UnimportantSfxDelay), ignoreTimeScale: true);

            unimportantLayer.IsBusy = false;
        }
    }

    bool IsLayerBusy(int layer)
    {
        bool isBusy = false;

        if (_unimportantLayers.TryGetValue(layer, out UnimportantLayer unimportantLayer))
            isBusy = unimportantLayer.IsBusy;

        return isBusy;
    }



    public async UniTask PlayImportantSFX(AudioClip audioClip, Vector3 position = default)
    {
        if (audioClip == null)
            return;

        if (_freeImportantSfxSources.Count == 0)
            return;

        AudioSource audioSource = _freeImportantSfxSources[0];
        _freeImportantSfxSources.Remove(audioSource);
        _busyImportantSfxSources.Add(audioSource);

        audioSource.clip = audioClip;
        if (position != default)
            audioSource.transform.position = position;
        audioSource.Play();

        await UniTask.Delay(TimeSpan.FromSeconds(audioClip.length), ignoreTimeScale: true);

        _freeImportantSfxSources.Add(audioSource);
        _busyImportantSfxSources.Remove(audioSource);
        audioSource.Stop();
    }



    public async UniTask PlayUnimportantSFX(AudioClip audioClip, Vector3 position = default, int layer = 0)
    {
        if (audioClip == null)
            return;

        if (IsLayerBusy(layer))
            return;

        if (_freeUnimportantSfxSources.Count == 0)
            return;

        AudioSource audioSource = _freeUnimportantSfxSources[0];
        _freeUnimportantSfxSources.Remove(audioSource);
        _busyUnimportantSfxSources.Add(audioSource);

        audioSource.clip = audioClip;
        if (position != default)
            audioSource.transform.position = position;
        audioSource.Play();

        UnimportantSfxDelay(layer);

        await UniTask.Delay(TimeSpan.FromSeconds(audioClip.length), ignoreTimeScale: true);

        _freeUnimportantSfxSources.Add(audioSource);
        _busyUnimportantSfxSources.Remove(audioSource);
        audioSource.Stop();
    }

    struct UnimportantLayer
    {
        public bool IsBusy;
    }
}