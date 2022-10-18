using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Panels;

namespace Meta.VR
{

    public class AvatarsSample : MonoBehaviour
    {

        [SerializeField] private ButtonWrapper defaultButton;
        [SerializeField] private ButtonWrapper selfieButton;
        [SerializeField] private ButtonWrapper videoButton;
        [SerializeField] private ButtonWrapper fadeInFadeOutButton;

        private void Awake()
        {
            defaultButton.AddListener(() => OnButtonClick(AvatarMode.Default));
            selfieButton.AddListener(() => OnButtonClick(AvatarMode.Selfie));
            videoButton.AddListener(() => OnButtonClick(AvatarMode.Video));
            fadeInFadeOutButton.AddListener(() => AvatarController.FadeInFadeOut(2));
        }

        private void OnDestroy()
        {
            defaultButton.RemoveListener();
            selfieButton.RemoveListener();
            videoButton.RemoveListener();
        }

        private void OnButtonClick(AvatarMode avatarMode)
        {
            AvatarController.SetMode(avatarMode);
        }

    }

}