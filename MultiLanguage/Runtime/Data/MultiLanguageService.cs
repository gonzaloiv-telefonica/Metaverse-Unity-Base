using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Meta.Global;

namespace Meta.MultiLanguage
{
    public class MultiLanguageService
    {
        public delegate void NewDictionaryAvailable(MultiLanguageDictionary newLanguageDictionary);
        public static event NewDictionaryAvailable newDictionaryAvailable;


        private MultiLanguageDictionary languageDictionary;
        public void CheckForLocalTextsOrUpdates(MultiLanguageDictionary oldLanguageDictionary)
        {
            if(LocalFileCheck(oldLanguageDictionary.name))
            {
                    if(IsConnected())
                    {
                        GetTextsFromService(oldLanguageDictionary);
                    }
                    else
                    {
                        newDictionaryAvailable(languageDictionary);
                    }

            }
            else
            {
                if(IsConnected())
                {
                    GetTextsFromService(oldLanguageDictionary);
                }
                else
                {
                    string json = JsonConvert.SerializeObject(oldLanguageDictionary);
                    File.WriteAllText(Application.persistentDataPath + "/"+oldLanguageDictionary.name+".json", json);
                }
            }



        }
        private async void GetTextsFromService(MultiLanguageDictionary oldLanguageDictionary)
        {
            string url = oldLanguageDictionary.urlLanguageService;

            // if(languageDictionary != null && !url.Equals(languageDictionary.urlLanguageService))
            // {
            //     url = languageDictionary.urlLanguageService;
            // }

            var getRequest = UnityWebRequest.Get(url);
            await getRequest.SendWebRequest();

            if (getRequest.result == UnityWebRequest.Result.ConnectionError || getRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(getRequest.error);
            }
            else
            {
                languageDictionary = JsonConvert.DeserializeObject<MultiLanguageDictionary>(getRequest.downloadHandler.text);

                if(languageDictionary.versionNumber > oldLanguageDictionary.versionNumber)
                {
                    File.WriteAllText(Application.persistentDataPath + "/"+languageDictionary.name+".json", JsonConvert.SerializeObject(languageDictionary));
                    if (newDictionaryAvailable != null)
                    {
                        newDictionaryAvailable(languageDictionary);
                    }
                }
                else
                {
                    if (newDictionaryAvailable != null)
                    {
                        newDictionaryAvailable(oldLanguageDictionary);
                    }
                }
            }
        }

        public bool LocalFileCheck(string fileName)
        {
            string path = Application.persistentDataPath + "/"+fileName+".json";
            if (File.Exists(path))
            {
                string jsonStr = File.ReadAllText(path); 
                languageDictionary = JsonConvert.DeserializeObject<MultiLanguageDictionary>(jsonStr);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsConnected()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return false;
            }else
            {
                return true;
            }
    
        }

    }
}
