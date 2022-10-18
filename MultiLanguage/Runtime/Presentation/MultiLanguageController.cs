using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Meta.Global.MonoSingleton;

namespace Meta.MultiLanguage
{
    public class MultiLanguageController : MonoSingleton<MultiLanguageController>
    {

        private static Language DefaultLang = Language.ES;

        #region EVENTS
        public delegate void LanguageChange(Language newLanguage);
        public static event LanguageChange OnLanguageChanged;
        #endregion

        #region CONST
        private const string GAME_LANG = "game_language";
        #endregion

        #region PRIVATE VARS

        private List<LocalizedText> listTraslation;
        [SerializeField]
        private Language gameLang = Language.ES;
        private string noTranslationText = "Translation missing for {0}";
        [SerializeField]
        private List<MultiLanguageDictionary> dictionaries;
        private List<Language> availableLangs;

        #endregion

        /// <summary>
        /// Change current language.
        /// Set default language if not initialized or recognized.
        /// </summary>
        /// <param name="langCode">Language code</param>
        public void setLanguage(string langCode)
        {
            setLanguage((Language)Enum.Parse(typeof(Language), langCode));
        }

        /// <summary>
        /// Change current language.
        /// Set default if language not initialized or recognized.
        /// </summary>
        /// <param name="langCode">Language code</param>
        public void setLanguage(Language langCode)
        {

            gameLang = langCode;
            PlayerPrefs.SetString(GAME_LANG, gameLang.ToString());

            if (OnLanguageChanged != null)
            {
                OnLanguageChanged(gameLang);
            }
        }

        /// <summary>
        /// Get key value in current language.
        /// </summary>
        /// <param name="key">Translation key. String should start with '^' character</param>
        /// <returns>Translation value</returns>
        public string getValue(string key)
        {
            return getValue(key, null);
        }

        /// <summary>
        /// Get key value in current language with additional params. 
        /// Currently not working.
        /// </summary>
        /// <param name="key">Translation key. String should start with '^' character and can contain params ex. {0} {1}...</param>
        /// <param name="parameters">Additional parameters.</param>
        /// <returns>Translation value</returns>
        public string getValue(string key, string[] parameters)
        {
            foreach (LocalizedText translation in listTraslation)
            {
                if (translation.textId == key)
                {
                    foreach (TextWithTranslation textTranslations in translation.translations)
                    {
                        if (textTranslations.language == gameLang)
                        {
                            if (parameters != null && parameters.Length > 0)
                            {
                                return string.Format(textTranslations.translatedText, parameters);
                            }
                            else
                            {
                                return textTranslations.translatedText;
                            }
                        }
                    }
                }

            }
            return string.Format(noTranslationText, key);
        }


        protected override void Init()
        {
            availableLangs = new List<Language>();
            listTraslation = new List<LocalizedText>();
            dictionaries.ForEach(dictionary => listTraslation.AddRange(ParseLanguage(dictionary)));
            string lang = null;
            if (!PlayerPrefs.HasKey(GAME_LANG))
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.Portuguese:
                        lang = "PT";
                        break;
                    case SystemLanguage.English:
                        lang = "EN";
                        break;
                    case SystemLanguage.German:
                        lang = "DE";
                        break;
                    case SystemLanguage.French:
                        lang = "FR";
                        break;
                    case SystemLanguage.Spanish:
                        lang = "ES";
                        break;
                }
            }
            else
            {
                lang = PlayerPrefs.GetString(GAME_LANG);
            }
            try
            {
                setLanguage(lang);
            }
            catch(Exception e)
            {   
                Debug.LogWarning(e.ToString());
                setLanguage(DefaultLang);
            }
            dictionaries.ForEach(dictionary => UpdateDictionary(dictionary));
        }

        private void UpdateDictionary(MultiLanguageDictionary dictionary)
        {
            if (!string.IsNullOrEmpty(dictionary.urlLanguageService))
            {
                multiLanguageService = new MultiLanguageService();
                MultiLanguageService.newDictionaryAvailable += ChangeTexts;
                multiLanguageService.CheckForLocalTextsOrUpdates(dictionary);
            }
        }

        private MultiLanguageService multiLanguageService;
        private List<LocalizedText> ParseLanguage(MultiLanguageDictionary scriptableObject)
        {

            return scriptableObject.listTraslation;
        }

        public void ChangeTexts(MultiLanguageDictionary newLanguageDictionary)
        {
            listTraslation = newLanguageDictionary.listTraslation;
            setLanguage(gameLang);
        }

        void OnDestroy()
        {
            try
            {
                MultiLanguageService.newDictionaryAvailable -= ChangeTexts;
            }
            catch (Exception e)
            {
                Debug.LogError("No listener active " +  e.ToString());
            }

        }
    }
}
