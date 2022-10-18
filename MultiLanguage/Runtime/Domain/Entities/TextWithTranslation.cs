using UnityEngine;
namespace Meta.MultiLanguage
{

    [System.Serializable]
    public class TextWithTranslation
    {
        public Language language;
        [TextArea]
        public string translatedText;



        public TextWithTranslation(Language languageAux, string translation)
        {
            language = languageAux;
            translatedText = translation;
        }
    }

}