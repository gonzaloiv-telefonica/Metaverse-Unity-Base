using System.Collections.Generic;


namespace Meta.MultiLanguage
{

    [System.Serializable]
    public struct LocalizedText
    {
        public string textId;
        public List<TextWithTranslation> translations;
    }

}