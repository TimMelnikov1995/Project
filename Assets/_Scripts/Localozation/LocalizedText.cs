using UnityEngine;

namespace IGM.Localization
{
    public abstract class LocalizedText : MonoBehaviour
    {
        protected void OnEnable()
        {
            UpdateText();
            ServiceLocator.Get<LocalizationService>().EOn_ChangeLanguage += UpdateText;
        }

        protected void OnDisable()
        {
            ServiceLocator.Get<LocalizationService>().EOn_ChangeLanguage -= UpdateText;
        }

        public abstract void UpdateText();
    }
}