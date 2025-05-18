using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using static SfxSoundsPool;

[Serializable]
public class AudioService
{
    public SfxAudio Sfx { get; private set; }

    Dictionary<string, Sound> _sounds = new();

    public AudioService()
    {
        Sfx = new SfxAudio();

        _sounds = Resources.Load<SfxSoundsPoolsCollection>("Audio/SfxSoundsPoolsCollection").Sounds;
    }

    public async UniTask PlayImportantSFX(string name, Vector3 position = default)
    {
        if (_sounds.TryGetValue(name, out Sound sound))
        {
            AudioClip clip = GetRandomClip(sound.Clips);
            await Sfx.PlayImportantSFX(clip, position);
        }
    }

    public async UniTask PlayUnimportantSFX(string name, Vector3 position = default, int layer = 0)
    {
        if (_sounds.TryGetValue(name, out Sound sound))
        {
            AudioClip clip = GetRandomClip(sound.Clips);
            await Sfx.PlayUnimportantSFX(clip, position, layer);
        }
    }

    public static AudioClip GetRandomClip(AudioClip[] audioClips)
    {
        if (audioClips == null)
            return null;

        if (audioClips.Length == 0)
            return null;

        int index = 0;

        if (audioClips.Length > 1)
            index = UnityEngine.Random.Range(0, audioClips.Length);

        return audioClips[index];
    }
}