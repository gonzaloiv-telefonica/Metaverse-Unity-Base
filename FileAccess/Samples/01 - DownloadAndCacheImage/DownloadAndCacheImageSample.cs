using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Meta.FileAccess
{

    public class DownloadAndCacheImageSample : MonoBehaviour
    {

        [SerializeField] private string url = "https://imgsv.imaging.nikon.com/lineup/dslr/df/img/sample/img_01.jpg";
        [SerializeField] private RawImage image;
        private RemoteFileClient remoteFileClient;
        private LocalFileClient localFileClient;

        private void Awake()
        {
            GlbImporter glbImporter = new GlbImporter();
            remoteFileClient = new RemoteFileClient(glbImporter);
            localFileClient = new LocalFileClient();
        }

        private void Start()
        {
            string fileName = Path.GetFileName(url);
            localFileClient.LoadImage(Application.persistentDataPath, fileName)
                .Then(bytes =>
                {
                    Texture2D tex = new Texture2D(100, 100);
                    tex.LoadImage(bytes);
                    image.texture = tex;
                })
                .Catch(ex =>
                {
                    Debug.LogException(ex);
                    remoteFileClient.GetTexture(url)
                        .Then(texture =>
                        {
                            Debug.LogWarning("REMOTE LOAD");
                            byte[] bytes = (texture as Texture2D).ToBytes(fileName);
                            localFileClient.SaveFile(fileName, bytes);
                            image.texture = texture;
                        })
                        .Catch(Debug.LogException);
                });
        }

    }

}