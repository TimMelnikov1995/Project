using UnityEngine;

[CreateAssetMenu(fileName = "SfxAudioSettings", menuName = "ScriptableObjects/Sfx Audio Settings", order = 1)]
public class SfxAudioSettings : ScriptableObject
{
    [field: SerializeField] public AudioSource ImportantSfxSourcePrefab;
    [field: SerializeField] public AudioSource UnimportantSfxSourcePrefab;
    [field: SerializeField] public int ImportantSfxCount = 25;
    [field: SerializeField] public int UnimportanSfxtCount = 25;
    [field: SerializeField] public float UnimportantSfxDelay = 0.2f;
}