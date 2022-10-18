using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace Meta.MultiLanguage
{

    [CreateAssetMenu(fileName = "MultiLanguage_Dictionary", menuName = "Meta/MultiLanguage/Dictionary")]
    public class MultiLanguageDictionary : ScriptableObject
    {
        public string urlLanguageService;
        public long versionNumber;
        public List<LocalizedText> listTraslation;


        public void SaveToFile()
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(Application.streamingAssetsPath + "/"+this.name+".json", json);
        }
    }

}