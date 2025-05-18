using TMPro;
using UnityEngine;

namespace IGM.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedFont_TMP : LocalizedText
    {
        [SerializeField, HideInInspector] TextMeshProUGUI m_text;

        void Awake()
        {
            m_text = GetComponent<TextMeshProUGUI>();
            UpdateText();
        }

        new void OnEnable()
        {
            base.OnEnable();
        }

        new void OnDisable()
        {
            base.OnDisable();
        }

        public override void UpdateText()
        {
            m_text.font = ServiceLocator.Get<LocalizationService>().GetFont();
            m_text.characterSpacing = ServiceLocator.Get<LocalizationService>().GetCharacterSpacing();
        }
    }
}