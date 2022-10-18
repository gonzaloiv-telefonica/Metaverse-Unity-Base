using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace Meta.SelfiePhotos
{
    public class SelfieLibrary
    {
        private static SelfieLibrary instance;

        public SelfieLibrary()
        {
            if (instance != null)
            {
                Debug.LogError("Cannot have two instances of singleton. Self destruction in 3...");
                return;
            }
            instance = this;

            string path = Application.persistentDataPath + "/SelfieLibrary/";
            if(!Directory.Exists(path)){
                Directory.CreateDirectory (path);
            }
            ReadSelfieLibraryFile();
        }

        public static SelfieLibrary Instance
        {
            get
            {
                if (instance == null)
                {
                    new SelfieLibrary();
                }

                return instance;
            }
        }

        public List<SelfiePhoto> selfieLibrary;

        public void AddPhoto(SelfiePhoto photo)
        {
            selfieLibrary.Add(photo);
            SaveSelfieLibraryToFile();
        }

        public bool RemovePhoto(SelfiePhoto photo)
        {
            int index = selfieLibrary.FindIndex(a => a.location == photo.location);
            selfieLibrary.RemoveAt(index);
            File.Delete(photo.location);
            SaveSelfieLibraryToFile();

            return true;
        }


        public void SaveSelfieLibraryToFile()
        {
            string json = JsonConvert.SerializeObject(selfieLibrary);
            File.WriteAllText(Application.persistentDataPath + "/SelfieLibrary/SelfieLibrary.json", json);
        }

        public void ReadSelfieLibraryFile()
        {
                string path = Application.persistentDataPath + "/SelfieLibrary/SelfieLibrary.json";

                if(File.Exists(path))
                {
                    string jsonStr = File.ReadAllText(path); 
                    selfieLibrary = JsonConvert.DeserializeObject<List<SelfiePhoto>>(jsonStr);

                    selfieLibrary.Sort((photo1, photo2) => System.DateTime.Compare(photo2.date, photo1.date));
                }
                else
                {
                    selfieLibrary = new List<SelfiePhoto>();
                }

        }
    }


}
