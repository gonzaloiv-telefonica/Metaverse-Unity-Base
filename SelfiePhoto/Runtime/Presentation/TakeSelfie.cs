using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Meta.Video;
using Meta.Panels;

namespace Meta.SelfiePhotos
{
    public class TakeSelfie : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private GameObject selfieStick;

        private int resWidth;
        private int resHeight;
        private bool takePicture = false;
        private bool isOpenByMenu = false;

        private void Awake()
        {
            resWidth = cam.targetTexture.width;
            resHeight = cam.targetTexture.height;
            SelfieLibrary.Instance.ReadSelfieLibraryFile();

            MenuGalleryPanelScript.returnAndOpenSelfieStick += OpenStickFromMenu;

        }

        void OnDestroy()
        {
            try
            {
                MenuGalleryPanelScript.returnAndOpenSelfieStick -= OpenStickFromMenu;
            }catch (Exception e)
            {
                Debug.LogError("No listener active " + e.ToString());
            }
        }

        private void OpenStickFromMenu()
        {
            if(MetaVideoPlayer.Instance.IsPlayingVideo())
                return;

            isOpenByMenu = true;
            selfieStick.SetActive(true);
        }

        private void LateUpdate()
        {
            if(takePicture && cam.isActiveAndEnabled)
            {
                Texture2D snapShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
                cam.Render();
                RenderTexture.active = cam.targetTexture;
                snapShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
                byte[] bytes = snapShot.EncodeToPNG();
                string filename = SnapShotName();
                System.IO.File.WriteAllBytes(filename, bytes);
                this.gameObject.GetComponent<AudioSource>().Play();

                SelfiePhoto newPhoto = new SelfiePhoto(resWidth, resHeight, filename);
                SelfieLibrary.Instance.AddPhoto(newPhoto);

                StartCoroutine(CloseAfterSelfie());
            }
        }

        private IEnumerator CloseAfterSelfie()
        {
            takePicture = false;
            yield return 0;
            if(isOpenByMenu)
            {
                isOpenByMenu = false;
                yield return new WaitForSeconds(1.3f);
                selfieStick.SetActive(false);
            }
        }
        
        private string SnapShotName()
        {
            string filePath = "";
            // #if UNITY_EDITOR
                filePath = Application.persistentDataPath+"/SelfieLibrary/"+Application.identifier;
            // #elif UNITY_ANDROID
                // filePath = "/sdcard/Oculus/Screenshots/"+Application.identifier;
                // filePath = "/sdcard/Pictures/"+Application.identifier;
            // #endif

            return string.Format("{0}-{1}.png", filePath, System.DateTime.Now.ToString("yyymmdd-HHmmss"));
        }

        public void TakePicture()
        {
            takePicture = true;
        }

        void Update()
        {
            if(MetaVideoPlayer.Instance.IsPlayingVideo())
                return;

            if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Space))
            {
                selfieStick.SetActive(true);
                isOpenByMenu = false;
            }

            if ((OVRInput.GetUp(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Escape)) && !isOpenByMenu)
            {
                selfieStick.SetActive(false);
            }

            if(selfieStick.activeSelf && (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) || Input.GetKeyDown(KeyCode.T)))
            {
                TakePicture();
            }
        }
    }
}