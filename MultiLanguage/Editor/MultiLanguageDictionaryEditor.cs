 using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Meta.MultiLanguage;

// namespace Meta.MultiLanguage.Editor
// {
    [CustomEditor(typeof(MultiLanguageDictionary))]
    public class MultiLanguageDictionaryEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (MultiLanguageDictionary)target;
    
                if(GUILayout.Button("Save to file", GUILayout.Height(40)))
                {
                    script.SaveToFile();
                }
            
        }

    }
// }
