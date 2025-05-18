using UnityEngine;

[CreateAssetMenu(fileName = "SfxAudioSettings", menuName = "ScriptableObjects/Sfx Audio Settings", order = 1)]
public class SfxAudioSettings : ScriptableObject
{
    [field: SerializeField] public AudioSource ImportantSfxSourcePrefab { get; private set; }
    [field: SerializeField] public AudioSource UnimportantSfxSourcePrefab { get; private set; }
    [field: SerializeField] public int ImportantSfxCount { get; private set; } = 25;
    [field: SerializeField] public int UnimportanSfxtCount { get; private set; } = 25;
    [field: SerializeField] public float UnimportantSfxDelay { get; private set; } = 0.2f;
}