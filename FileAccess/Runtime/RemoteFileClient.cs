using System;
using System.Collections.Generic;
using Proyecto26;
using RSG;
using UnityEngine;
using UnityEngine.Networking;

namespace Meta.FileAccess
{

    public class RemoteFileClient
    {

        private GlbImporter glbImporter;

        public RemoteFileClient(GlbImporter glbImporter)
        {
            this.glbImporter = glbImporter;
        }

        public IPromise<Texture> GetTexture(string url)
        {
            Promise<Texture> promise = new Promise<Texture>();
            RequestHelper requestHelper = requestHelper = new RequestHelper { Uri = url };
            requestHelper.DownloadHandler = new DownloadHandlerTexture();
            RestClient.Get(requestHelper)
                .Then(response =>
                {
                    DownloadHandlerTexture downloadHandlerTexture = (DownloadHandlerTexture)response.Request.downloadHandler;
                    Texture texture = downloadHandlerTexture.texture;
                    texture.name = new Uri(url).PathAndQuery;
                    promise.Resolve(texture);
                }).Catch(promise.Reject);
            return promise;
        }

        public Promise<GameObject> GetGlb(string url)
        {
            return glbImporter.Import(url);
        }

    }

}