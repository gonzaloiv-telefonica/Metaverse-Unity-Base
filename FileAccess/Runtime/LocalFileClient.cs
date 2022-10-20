using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using MEC;
using RSG;
using System;

namespace Meta.FileAccess
{

    public class LocalFileClient
    {

        private static readonly string OculusGalleryPath = "/sdcard/Oculus/Screenshots/" + Application.identifier;

        public void SaveFile(string fileName, byte[] bytes, string basePath = "")
        {
            if (string.IsNullOrEmpty(basePath))
                basePath = Application.persistentDataPath;
            string path = Path.Combine(basePath, fileName);
            File.WriteAllBytes(path, bytes);
        }

        public void SaveFileToOculusGallery(string fileName, byte[] bytes)
        {
            if (!Directory.Exists(OculusGalleryPath))
                Directory.CreateDirectory(OculusGalleryPath);
            SaveFile(fileName, bytes, OculusGalleryPath);
        }

        public Promise<byte[]> LoadImage(string basePath, string fileName)
        {
            string path = Path.Combine(basePath, fileName);
            if (path.Contains("://") || path.Contains(":///")) // ? In case we're using Android streaming assets folder
            {
                return MobileLoad(path);
            }
            else
            {
                return StandaloneLoad(path);
            }
        }

        public Promise<byte[]> LoadImageFromStreamingAssets(string fileName)
        {
            return LoadImage(Application.streamingAssetsPath, fileName);
        }

        private Promise<byte[]> MobileLoad(string path)
        {
            Promise<byte[]> promise = new Promise<byte[]>();
            IEnumerator<float> Routine()
            {
                UnityWebRequest www = UnityWebRequest.Get(path);
                www.SendWebRequest();
                while (!www.isDone)
                    yield return Timing.WaitForOneFrame;
                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    promise.Reject(new Exception($"File on path {path} could not be found"));
                }
                else
                {
                    promise.Resolve(www.downloadHandler.data);
                }
            }
            Timing.RunCoroutine(Routine());
            return promise;
        }

        private Promise<byte[]> StandaloneLoad(string path)
        {
            Promise<byte[]> promise = new Promise<byte[]>();
            try
            {
                promise.Resolve(File.ReadAllBytes(path));
            }
            catch (Exception exception)
            {
                promise.Reject(exception);
            }
            return promise;
        }

    }

}