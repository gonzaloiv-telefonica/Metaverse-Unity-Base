using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Meta.Panels
{

    public class BasicPanel : MonoBehaviour
    {

        [SerializeField] private ButtonWrapper closeButton;
        [SerializeField] private ButtonWrapper minimizeButton;
        [SerializeField] private ButtonWrapper maximizeButton;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject secondaryPanel;

        private Action onClose;

        protected virtual void Awake()
        {
            mainPanel.SetActive(true);
            secondaryPanel.SetActive(false);
        }

        protected virtual void OnEnable()
        {
            if (closeButton != null)
                closeButton.AddListener(Close);
            if (minimizeButton != null)
                minimizeButton.AddListener(Minimize);
            if (maximizeButton != null)
                maximizeButton.AddListener(Maximize);
        }

        protected virtual void OnDisable()
        {
            if (closeButton != null)
                closeButton.RemoveListener();
            if (minimizeButton != null)
                minimizeButton.RemoveListener();
            if (maximizeButton != null)
                maximizeButton.RemoveListener();
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            if (onClose != null)
                onClose();
        }

        public virtual void Minimize()
        {
            mainPanel.transform.DOScale(Vector3.zero, 0.3f)
                .OnComplete(() =>
                {
                    mainPanel.gameObject.SetActive(false);
                    secondaryPanel.gameObject.SetActive(true);
                });
        }

        public virtual void Maximize()
        {
            secondaryPanel.gameObject.SetActive(false);
            mainPanel.gameObject.SetActive(true);
            mainPanel.transform.DOScale(Vector3.one, 0.3f);
        }

        public virtual void SetOnClose(Action onClose)
        {
            this.onClose = onClose;
        }

    }

}