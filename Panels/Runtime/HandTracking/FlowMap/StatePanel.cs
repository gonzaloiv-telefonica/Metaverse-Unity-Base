using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Meta.FlowControl;
using Meta.Global;
using System;

namespace Meta.Panels
{

    public class StatePanel : BaseView
    {

        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI nameLabel;
        [SerializeField] private ButtonWrapper button;

        private Action onClick;

        private void Awake()
        {
            button.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            button.RemoveListener();
        }

        private void OnClick()
        {
            onClick();
        }

        public void Show(StateData data, Action onClick, bool isCurrentState)
        {
            base.Show();
            image.sprite = data.sprite;
            nameLabel.text = data.name;
            this.onClick = onClick;
            button.SetIsInteractable(!isCurrentState);
        }

    }

}