using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Global;
using Meta.Panels;
using System;

namespace Meta.SelfiePhotos
{
    public class SelfieTestSceneScript : MonoBehaviour 
    {
        [SerializeField] private GameObject menuItem;
        [SerializeField] private GameObject galleryPanel;

        private void Awake()
        {
            MenuSelfieSelectorScript.openGalleryDelegate += OpenGallery;                
        }

        void OnDestroy()
        {
            try
            {
                MenuSelfieSelectorScript.openGalleryDelegate -= OpenGallery;   
            }catch (Exception e)
            {
                Debug.LogError("No listener active " +  e.ToString());
            }

        }

        private void OpenGallery(){
            menuItem.SetActive(false);
            galleryPanel.SetActive(true);
        }

        void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.Start) || Input.GetKeyDown(KeyCode.M))
            {
                menuItem.SetActive(true);
                galleryPanel.SetActive(false);
            }

        }
    }
}