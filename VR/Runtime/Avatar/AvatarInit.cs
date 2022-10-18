using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using System;
using Oculus.Platform.Models;
using RSG;

namespace Meta.VR
{

    public class AvatarInit : MonoBehaviour
    {

        [SerializeField] private string token;
        [SerializeField] private SampleAvatarEntity sampleAvatarEntity;

        // Based on: OvrPlatformInit.cs
        public Promise<string> Initialize()
        {

            Promise<string> promise = new Promise<string>();
            Core.AsyncInitialize(token).OnComplete(InitializeComplete);

            void InitializeComplete(Message<PlatformInitialize> msg)
            {
                if (msg.Data.Result != PlatformInitializeResult.Success)
                {
                    promise.Reject(new Exception("Error on InitializeComplete"));
                }
                else
                {
                    Entitlements.IsUserEntitledToApplication().OnComplete(CheckEntitlement);
                }
            }

            void CheckEntitlement(Message msg)
            {
                if (msg.IsError == false)
                {
                    Users.GetAccessToken().OnComplete(GetAccessTokenComplete);
                }
                else
                {
                    promise.Reject(new Exception("Error on CheckEntitlement"));
                }
            }

            void GetAccessTokenComplete(Message<string> msg)
            {

                if (String.IsNullOrEmpty(msg.Data))
                {
                    promise.Reject(new Exception("Error on GetAccessTokenComplete"));
                }
                else
                {
                    Users.GetLoggedInUser().OnComplete(GetLoggedInUserComplete);
                }
            }

            void GetLoggedInUserComplete(Message<User> msg)
            {
                if (msg.Data == null)
                {
                    promise.Reject(new Exception("Error on GetLoggedInUserComplete"));
                }
                else
                {
                    sampleAvatarEntity.OnUserAvatarLoadedEvent.AddListener(data => promise.Resolve(msg.Data.ID.ToString()));
                    sampleAvatarEntity.LoadRemoteUserCdnAvatar(msg.Data.ID);
                }
            }

            return promise;

        }

    }

}

