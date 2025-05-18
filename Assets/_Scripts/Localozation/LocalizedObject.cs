using UnityEngine;

namespace IGM.Localization
{
    public class LocalizedObject : LocalizedText
    {
        [SerializeField] SingleLocalizedObject[] m_objects;

        void Awake()
        {
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

        void DisableAll()
        {
            foreach (var obj in m_objects)
            {
                obj.Object.SetActive(false);
            }
        }

        public override void UpdateText()
        {
            DisableAll();

            string lang = ServiceLocator.Get<LocalizationService>().GetCurrentLang();
            foreach (var obj in m_objects)
            {
                if(obj.Lang == lang)
                    obj.Object.SetActive(true);
            }
        }



        public class SingleLocalizedObject
        {
            public string Lang;
            public GameObject Object;
        }
    }
}