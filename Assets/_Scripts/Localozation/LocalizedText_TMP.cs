using TMPro;
using UnityEngine;

namespace IGM.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText_TMP : LocalizedText
    {
        [SerializeField] string m_textName;
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
            m_text.text = ServiceLocator.Get<LocalizationService>().GetTextByName(m_textName);
        }
    }
}