using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using OculusSampleFramework;
using DG.Tweening;

namespace Meta.Panels
{

    public class ButtonWrapper : MonoBehaviour, IInteractableComponent
    {

        [Header("ButtonWrapper")]
        public bool isInteractable = true;

        [SerializeField] protected AudioSource audioSource;

        protected Action onInteraction;
        protected InteractableState interactableState;
        protected InteractableTool tool;

        protected Button button;
        protected ButtonController buttonController;

        protected Button Button => button == null ? button = GetComponent<Button>() : button;
        protected ButtonController ButtonController => buttonController == null ?
            buttonController = GetComponent<ButtonController>() : buttonController;

        public void AddListener(Action onInteraction)
        {
            this.onInteraction = onInteraction;
            Button.onClick.AddListener(OnClick);
            ButtonController.InteractableStateChanged.AddListener(OnInteractableStateChanged);
        }

        public void RemoveListener()
        {
            Button.onClick.RemoveListener(OnClick);
            ButtonController.InteractableStateChanged.RemoveListener(OnInteractableStateChanged);
        }

        protected virtual void OnClick()
        {
            float animationTime = 0.2f;
            Button.targetGraphic.color = Button.colors.pressedColor;
            Button.targetGraphic.DOColor(Button.colors.normalColor, animationTime)
                .OnComplete(() =>
                {
                    if (onInteraction != null)
                        onInteraction();
                });
            PlayAudio();
            if (tool != null)
                tool.DeFocus();
        }

        protected virtual void OnInteractableStateChanged(InteractableStateArgs eventArgs)
        {
            if (isInteractable && eventArgs.NewInteractableState == InteractableState.ActionState)
                OnClick();
            interactableState = eventArgs.NewInteractableState;
            tool = eventArgs.Tool;
        }

        protected virtual void Update()
        {
            Button.targetGraphic.color = interactableState > InteractableState.Default ?
                Button.colors.highlightedColor : Button.colors.normalColor;
        }

        public void SetIsInteractable(bool isInteractable)
        {
            this.isInteractable = isInteractable;
            Button.interactable = isInteractable;
            Button.targetGraphic.color = Button.colors.disabledColor;
        }

        protected virtual void PlayAudio()
        {
            if (audioSource != null)
                audioSource.Play();
        }

    }

}