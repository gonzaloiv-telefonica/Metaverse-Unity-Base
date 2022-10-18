using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;

namespace Meta.SelfiePhotos
{
    public class MenuGalleryPanelScript : MonoBehaviour
    {

        [SerializeField] private GameObject parentPanels;
        [SerializeField] private GameObject panelPrefab;
        [SerializeField] private GameObject panelPhoto;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private GameObject openSelfieStick;
        [SerializeField] private Button btnPrev;
        [SerializeField] private Button btnNext;
        [SerializeField] private SimpleScrollSnap scrollView;

        public delegate void ReturnAndOpenSelfieStick();
        public static event ReturnAndOpenSelfieStick returnAndOpenSelfieStick;

        private void Awake()
        {
            SelfieLibrary.Instance.ReadSelfieLibraryFile();
        }

        void OnEnable()
        {

            if(scrollView.NumberOfPanels > 0)
            {
                for(int i = 0; i <= scrollView.NumberOfPanels; i++)
                {
                    Debug.Log("Borro panel " + i);
                    scrollView.RemoveFromFront();
                }
            }

            for(int i = 0; i<Mathf.Ceil((SelfieLibrary.Instance.selfieLibrary.Count/8f)); i++)
            {
                scrollView.AddToBack(panelPrefab);
            }


            int numberOfPhotos = 0;
            int indexPanel = 0;
            foreach(SelfiePhoto photo in SelfieLibrary.Instance.selfieLibrary)
            {
                GameObject photGo = null;
                if(numberOfPhotos == 0)
                {
                    photGo = Instantiate(openSelfieStick);
                    photGo.GetComponent<Button>().onClick.AddListener(() => OpenSelfieStick());
                }
                else
                {
                    photGo = Instantiate(cellPrefab);
                    StartCoroutine(photGo.GetComponent<SelfiePhotoCell>().PaintPhoto(photo, this));

                }

                photGo.transform.SetParent(scrollView.Panels[indexPanel].gameObject.transform);
                photGo.transform.localScale = Vector3.one;
                photGo.transform.localPosition = new Vector3(photGo.transform.localPosition.x, 0f, 0f);
                photGo.transform.localRotation = cellPrefab.transform.localRotation;
                photGo.SetActive(true);
                numberOfPhotos++;

                if(numberOfPhotos % 8 == 0 && numberOfPhotos!=0)
                    indexPanel++;
            }

            PanelChange();
        }

        public void PanelChange()
        {
            btnPrev.interactable = true;
            btnNext.interactable = true;

            if(scrollView.CenteredPanel < 1)
                btnPrev.interactable = false;

            if(scrollView.CenteredPanel >= (scrollView.NumberOfPanels-1))
                btnNext.interactable = false;
        }

        public void ShowPhotoPanel(SelfiePhoto photoPassed)
        {
            panelPhoto.SetActive(true);
            panelPhoto.GetComponent<MenuShowPhoto>().SetPhoto(photoPassed);
            this.gameObject.SetActive(false);
        }


        private void OpenSelfieStick()
        {
            Debug.Log("OpenSelfieStick");
            if (returnAndOpenSelfieStick != null)
            {
                Debug.Log("OpenSelfieStick 1");
                returnAndOpenSelfieStick();
                this.gameObject.SetActive(false);
            }
        }
    }
}
