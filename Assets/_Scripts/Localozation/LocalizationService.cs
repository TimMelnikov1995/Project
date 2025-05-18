using System;
using System.Collections.Generic;
using UnityEngine;

namespace IGM.Localization
{
    public class LocalizationService
    {
        Language[] _languages;
        [SerializeField, HideInInspector] string[] allTexts;

        string _text;
        string _currentLang;

        public Action EOn_ChangeLanguage;

        public LocalizationService()
        {
            _languages = Resources.Load<LocalizationSettings>("LocalizationSettings").Languages;
        }

        void GetTexts()
        {
            allTexts = _text.Split("//");
            for (int i = 0; i < allTexts.Length; i++)
            {
                allTexts[i] = allTexts[i].Trim();
            }
        }

        public void CheackTextSameNames()
        {
            GetTexts();

            var dict = new Dictionary<string, int>();
            bool allGood = true;

            for (int i = 0; i < allTexts.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    dict.TryGetValue(allTexts[i], out int count);
                    dict[allTexts[i]] = count + 1;
                }
            }

            foreach (var pair in dict)
            {
                if (pair.Value > 1)
                {
                    Debug.LogError("Name <<" + pair.Key + ">> occurred " + pair.Value + " times.");
                    allGood = false;
                }
            }

            if (allGood)
                Debug.Log("No same names");
        }


        public void SetLanguage(string language)
        {
            _currentLang = language;
            _text = _languages[GetLangIndex(_currentLang)].Text;

            GetTexts();

            EOn_ChangeLanguage?.Invoke();
        }

        public string GetCurrentLang()
        {
            return _currentLang;
        }

        public int GetLangIndex(string lang)
        {
            int langIndex = 0;

            for (int i = 0; i < _languages.Length; i++)
            {
                if (_languages[i].Lang == lang)
                {
                    langIndex = i;
                    break;
                }
            }

            return langIndex;
        }

        public string GetTextByName(string textName)
        {
            int index = Array.IndexOf(allTexts, textName);
            return allTexts[index + 1];
        }

        public TMPro.TMP_FontAsset GetFont()
        {
            int langIndex = GetLangIndex(_currentLang);
            return _languages[langIndex].Font;
        }

        public float GetCharacterSpacing()
        {
            int langIndex = GetLangIndex(_currentLang);
            return _languages[langIndex].CharacterSpacing;
        }




        [Serializable]
        public class Language
        {
            [SerializeField] string m_lang;

            [Tooltip("Format:\n" +
            "Name1//\n" +
            "Text1\n" +
            "//Name2//\n" +
            "Text2\n" +
            "//Name3//\n" +
            "Text3")]
            [SerializeField] UnityEngine.TextAsset m_text;
            [SerializeField] TMPro.TMP_FontAsset m_font;
            [SerializeField] float m_characterSpacing = 0;

            public string Lang => m_lang;
            public string Text => m_text.text;
            public TMPro.TMP_FontAsset Font => m_font;
            public float CharacterSpacing => m_characterSpacing;
        }
    }
}