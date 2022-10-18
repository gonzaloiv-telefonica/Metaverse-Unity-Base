using UnityEngine;
using System.Collections;
using TMPro;

namespace Meta.MultiLanguage
{
    public class MultiLanguageTextMesh : MonoBehaviour
    {

        [SerializeField] private string key = "";
        [SerializeField] private string[] parameters;

        private TMP_Text text;
        private MeshRenderer rend;
        private bool initialized = false;
        private bool isValidKey = false;
        private TextAlignmentOptions defaultAlignment;

        private void OnEnable()
        {
            if (!initialized)
                Init();
            UpdateTranslation();
        }

        private void OnDestroy()
        {
            if (initialized)
                MultiLanguageController.OnLanguageChanged -= OnLanguageChanged;
        }

        private void UpdateTranslation()
        {
            if (text)
            {
                if (!isValidKey)
                {
                    if (key.StartsWith("^"))
                        isValidKey = true;
                }
                text.text = MultiLanguageController.Instance.getValue(key, parameters);
            }
        }

        private void Init()
        {
            text = GetComponent<TMP_Text>();
            rend = GetComponent<MeshRenderer>();
            defaultAlignment = text.alignment;
            initialized = true;
            MultiLanguageController.OnLanguageChanged += OnLanguageChanged;
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
                Debug.LogWarning(string.Format("{0}: Text component was not found!", this));
        }

        private void OnLanguageChanged(Language newLang)
        {
            UpdateTranslation();
        }

    }

}