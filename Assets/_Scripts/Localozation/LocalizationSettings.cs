using UnityEngine;

namespace IGM.Localization
{
    [CreateAssetMenu(fileName = "LocalizationSettings", menuName = "ScriptableObjects/Localization Settings", order = 1)]
    public partial class LocalizationSettings : ScriptableObject
    {
        [field: SerializeField] public LocalizationService.Language[] Languages;
    }
}