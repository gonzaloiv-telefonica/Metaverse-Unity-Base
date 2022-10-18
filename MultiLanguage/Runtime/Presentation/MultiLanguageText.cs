using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Meta.MultiLanguage
{
    public class MultiLanguageText : MonoBehaviour
    {
        private string key = "";
        private Text text;
        private bool initialized = false;
        private bool isValidKey = false;
        private TextAnchor defaultAlignment;

        [SerializeField]
        private string[] parameters;

        void OnEnable()
        {
            if (!initialized)
                init();

            updateTranslation();
        }

        void OnDestroy()
        {
            if (initialized)
            {
                MultiLanguageController.OnLanguageChanged -= onLanguageChanged;
            }
        }

        /// <summary>
        /// Change text in Text component.
        /// </summary>
        private void updateTranslation()
        {
            if (text)
            {
                if (!isValidKey)
                {
                    key = text.text;

                    if (key.StartsWith("^"))
                    {
                        isValidKey = true;
                    }
                }

                text.text =  MultiLanguageController.Instance.getValue(key, parameters);
            }
        }

        /// <summary>
        /// Update translation text.
        /// </summary>
        /// <param name="invalidateKey">Force to invalidate current translation key</param>
        public void updateTranslation(bool invalidateKey = false)
        {
            if (invalidateKey)
            {
                isValidKey = false;
            }

            updateTranslation();
        }

        /// <summary>
        /// Init component.
        /// </summary>
        private void init()
        {
            text = GetComponent<Text>();
            defaultAlignment = text.alignment;
            key = text.text;
            initialized = true;


            MultiLanguageController.OnLanguageChanged += onLanguageChanged;

            if (!key.StartsWith("^"))
            {
                Debug.LogWarning(string.Format("{0}: Translation key was not found! Found {1}", this, key));
                isValidKey = false;
            }
            else
            {
                isValidKey = true;
            }

            if (!text)
            {
                Debug.LogWarning(string.Format("{0}: Text component was not found!", this));
            }
        }

        private void onLanguageChanged(Language newLang)
        {
            updateTranslation();
        }


        private TextAnchor getAnchorFromAlignment(TextAlignment alignment)
        {
            switch (defaultAlignment)
            {
                case TextAnchor.UpperLeft:
                //case TextAnchor.UpperCenter:
                case TextAnchor.UpperRight:
                    if (alignment == TextAlignment.Left)
                        return TextAnchor.UpperLeft;
                    else if (alignment == TextAlignment.Right)
                        return TextAnchor.UpperRight;
                    break;
                case TextAnchor.MiddleLeft:
                //case TextAnchor.MiddleCenter:
                case TextAnchor.MiddleRight:
                    if (alignment == TextAlignment.Left)
                        return TextAnchor.MiddleLeft;
                    else if (alignment == TextAlignment.Right)
                        return TextAnchor.MiddleRight;
                    break;
                case TextAnchor.LowerLeft:
                //case TextAnchor.LowerCenter:
                case TextAnchor.LowerRight:
                    if (alignment == TextAlignment.Left)
                        return TextAnchor.LowerLeft;
                    else if (alignment == TextAlignment.Right)
                        return TextAnchor.LowerRight;
                    break;
            }

            return defaultAlignment;
        }
    }
}