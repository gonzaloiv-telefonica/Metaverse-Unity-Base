using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace Meta.VR
{

    public abstract class BaseClickable : BaseInteractable, IRaySelectable
    {

        private const float clickSecs = 0.2f;

        protected override void SetInteractorListeners(InteractorBehaviour interactor)
        {
            interactor.onPrimaryClick += OnPrimaryClickRoutine;
        }

        public void OnPrimaryClickRoutine()
        {
            IEnumerator<float> ClickRoutine()
            {
                SetViewState(InteractableState.Click);
                yield return Timing.WaitForSeconds(clickSecs);
                SetViewState(state);
                OnPrimaryClick();
            }
            Timing.RunCoroutine(ClickRoutine().CancelWith(gameObject));
        }

        protected abstract void OnPrimaryClick();

        protected override void RemoveInteractorListeners(InteractorBehaviour interactor)
        {
            interactor.onPrimaryClick -= OnPrimaryClickRoutine;
        }

    }

}