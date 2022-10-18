using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Global;
using Meta.Panels;

public class MenuSelfieSelectorScript : BaseView
{
    // Start is called before the first frame update [SerializeField] private GameObject menuItem;

        [SerializeField] private ButtonWrapper selfieStick;
        [SerializeField] private ButtonWrapper openGallery;


        // public delegate void ReturnAndOpenSelfieStick();
        // public static event ReturnAndOpenSelfieStick returnAndOpenSelfieStick;

        public delegate void OpenGalleryDelegate();
        public static event OpenGalleryDelegate openGalleryDelegate;

        private void Awake()
        {
            // selfieStick.AddListener(() => OpenSelfieStick());
            openGallery.AddListener(() => OpenGallery());
        }

        // private void OpenSelfieStick()
        // {
        //     if (returnAndOpenSelfieStick != null)
        //     {
        //         returnAndOpenSelfieStick();
        //         this.gameObject.SetActive(false);
        //     }
        // }

        private void OpenGallery()
        {
            if (openGalleryDelegate != null)
            {
                openGalleryDelegate();
                this.gameObject.SetActive(false);
            }
        }
}
