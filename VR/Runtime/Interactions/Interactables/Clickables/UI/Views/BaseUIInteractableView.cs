using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using Meta.Audio;

namespace Meta.VR
{

    public abstract class BaseUIInteractableView : MonoBehaviour, IInteractableView
    {

        private const float OnHoverSecs = 0.6f;

        [SerializeField] private ClipData onHoverClipData;
        [SerializeField] private ClipData onClickClipData;

        protected bool isInHoverState;

        private InteractableState previousState;

        private void OnEnable()
        {
            isInHoverState = false;
        }

        public void SetState(InteractableState state)
        {
            switch (state)
            {
                case InteractableState.Click:
                    SfxPlayer.Play(onClickClipData);
                    OnClick();
                    break;
                case InteractableState.Hover:
                    PlayOnHoverClip();
                    OnHover();
                    break;
                case InteractableState.Disabled:
                    OnDisabled();
                    break;
                case InteractableState.Enabled:
                    OnEnabled();
                    break;
            }
            previousState = state;
        }

        private void PlayOnHoverClip()
        {
            if (isInHoverState || previousState == InteractableState.Hover)
                return;
            SfxPlayer.Play(onHoverClipData);
            IEnumerator<float> IsInHoverStateRoutine()
            {
                isInHoverState = true;
                yield return Timing.WaitForSeconds(OnHoverSecs);
                isInHoverState = false;
            }
            Timing.RunCoroutine(IsInHoverStateRoutine().CancelWith(gameObject));
        }

        protected abstract void OnClick();

        protected abstract void OnHover();

        protected abstract void OnDisabled();

        protected abstract void OnEnabled();

    }

}