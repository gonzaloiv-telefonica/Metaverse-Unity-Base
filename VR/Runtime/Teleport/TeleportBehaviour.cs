using UnityEngine;
using Meta.Global;
using DG.Tweening;
using Meta.Audio;

namespace Meta.VR
{

    public class TeleportBehaviour : MonoBehaviour
    {

        [SerializeField] private float animationSecs = 2f;
        [SerializeField] private TeleportPoint origin;
        [SerializeField] private TeleportPoint destination;
        [SerializeField] private ClipData clipData;

        public delegate void OnTeleportCompleteDelegate();
        public static event OnTeleportCompleteDelegate onTeleportCompleteDelegate;

        private void Awake()
        {
            origin.onAvatarEnter += () => OnAvatarEnter(destination.EndPoint);
            destination.onAvatarEnter += () => OnAvatarEnter(origin.EndPoint);
        }

        protected virtual void OnAvatarEnter(Transform endPoint)
        {
            if (clipData.HasClip)
                SfxPlayer.Play(clipData);
            AvatarController.FadeIn(animationSecs / 2)
                .OnComplete(() =>
                {
                    AvatarController.SetWorldPose(endPoint.ToWorldPose());
                    AvatarController.FadeOut(animationSecs / 2);
                    OnTeleportComplete();
                });
        }

        protected virtual void OnTeleportComplete()
        {
            onTeleportCompleteDelegate?.Invoke();
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

    }

}