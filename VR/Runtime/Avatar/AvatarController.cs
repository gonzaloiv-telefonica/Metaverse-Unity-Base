using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Global.MonoSingleton;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Meta.Global;
using Oculus.Avatar2;
using MEC;

namespace Meta.VR
{

    public class AvatarController : MonoBehaviour
    {

        public static AvatarController Instance => instance ??= FindObjectOfType<AvatarController>();
        private static AvatarController instance;

        private const float DefaultCharacterRadius = 0.2f;
        private const float SelfieCharacterRadius = 1.2f;

        [SerializeField] private OVRPlayerController playerController;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Image fadeImage;
        [SerializeField] private Color fadeColor;
        [SerializeField] private Transform maskPoint;
        [SerializeField] private AvatarInit avatarInit;
        [SerializeField] private List<InteractorBehaviour> interactors;
        [SerializeField] private JumpInput jumpInput;

        private OvrAvatarGazeTarget gazeTarget;
        private string id;
        private float initialAcceleration;

        public static bool IsPlayerControllerActive => Instance.playerController.enabled;
        public static Vector3 CurrentPosition => Instance.playerController.transform.position;
        public static string PlatformId => Instance.id;
        public static bool IsAvatarInit => !string.IsNullOrEmpty(Instance.id);
        private static Color TransparentColor => new Color(Instance.fadeColor.r, Instance.fadeColor.g, Instance.fadeColor.b, 0);
        public static Transform MaskPoint
        {
            get
            {
                if (Instance.gazeTarget != null)
                    return Instance.gazeTarget.transform;
                OvrAvatarGazeTarget gazeTarget = Instance.GetComponentInChildren<OvrAvatarGazeTarget>();
                if (gazeTarget != null)
                    return gazeTarget.transform;
                return Instance.maskPoint;
            }
        }
        public static bool IsMoving
        {
            get
            {
                if (OVRManager.isHmdPresent)
                {
                    bool isMoving = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick) != Vector2.zero;
                    return isMoving;
                }
                else
                {
                    bool isMoving = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
                    return isMoving;
                }
            }
        }

        private void Awake()
        {
            avatarInit.Initialize()
                .Then(id => this.id = id)
                .Catch(Debug.LogException);
            initialAcceleration = playerController.Acceleration;
        }

        public static void SetPlayerControllerActive(bool isActive)
        {
            Instance.playerController.enabled = isActive;
            Instance.characterController.enabled = isActive;
            Instance.jumpInput.enabled = isActive;
        }

        public static void SetCharacterRadius(float radius)
        {
            Instance.characterController.radius = radius;
            Instance.characterController.center = new Vector3(0, radius, 0);
        }

        public static void SetMode(AvatarMode mode)
        {
            bool isPlayerControllerActive = true;
            float radius = DefaultCharacterRadius;
            switch (mode)
            {
                case AvatarMode.Selfie:
                    radius = SelfieCharacterRadius;
                    break;
                case AvatarMode.Video:
                    isPlayerControllerActive = false;
                    break;
            }
            SetPlayerControllerActive(isPlayerControllerActive);
            SetCharacterRadius(radius);
        }

        public static Tweener FadeIn(float secs = 0.25f)
        {
            Instance.interactors.ForEach(interactor => interactor.SetVisibility(false));
            DOTween.Kill(Instance.fadeImage);
            return Instance.fadeImage.DOColor(Instance.fadeColor, secs)
                .SetEase(Ease.Linear);
        }

        public static Tweener FadeOut(float secs = 0.25f)
        {
            Instance.interactors.ForEach(interactor => interactor.SetVisibility(true));
            DOTween.Kill(Instance.fadeImage);
            return Instance.fadeImage.DOColor(TransparentColor, secs)
               .SetEase(Ease.Linear);
        }

        public static void FadeInFadeOut(float secs, Action onComplete = null)
        {
            DOTween.Sequence()
                .Append(FadeIn(secs / 3))
                .AppendInterval(secs / 3)
                .Append(FadeOut(secs / 3))
                .AppendCallback(() => onComplete?.Invoke());
        }

        public static void SetWorldPose(Pose pose)
        {
            SimpleCameraController controller = null;
            if (!OVRManager.isHmdPresent)
            {
                controller = Instance.GetComponentInChildren<SimpleCameraController>(true);
                if (controller != null)
                    controller.gameObject.SetActive(false);
            }
            Instance.characterController.enabled = false;
            Instance.characterController.transform.position = pose.position;
            Instance.characterController.transform.rotation = pose.rotation;
            Instance.characterController.enabled = true;
            if (controller != null)
                controller.gameObject.SetActive(true);
        }

        public static void SetInteractorsVisibility(bool visible)
        {
            Instance.interactors.ForEach(interactor => interactor.SetVisibility(visible));
        }

        public static void ApplyAccelerationRatio(float ratio = 1f)
        {
            Instance.playerController.Acceleration = Instance.initialAcceleration * ratio;
        }

    }

    public enum AvatarMode
    {
        Default,
        Selfie,
        Video
    }

}