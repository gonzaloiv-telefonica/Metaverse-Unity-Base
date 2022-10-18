using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MEC;

namespace Meta.VR
{

    public class TouchableBehaviour : BaseInteractable, ISphereSelectable
    {

        private const float CooldownSecs = 0.1f;

        private List<InteractorBehaviour> previousInteractors;
        private bool isCoolingDown;

        public Action onTouch = () => { };

        private void OnEnable()
        {
            isCoolingDown = false;
        }

        protected override void SetInteractorListeners(InteractorBehaviour interactor) { }
        protected override void RemoveInteractorListeners(InteractorBehaviour interactor) { }

        protected virtual void Update()
        {
            if (isCoolingDown)
                return;
            if (HasInteractors)
            {
                if (!interactors.AreEqualsTo(previousInteractors))
                    Timing.RunCoroutine(CooldownRoutine());
            }
            else
            {
                previousInteractors = null;
            }
        }

        private IEnumerator<float> CooldownRoutine()
        {
            previousInteractors = interactors.Clone();
            onTouch.Invoke();
            isCoolingDown = true;
            yield return Timing.WaitForSeconds(CooldownSecs);
            isCoolingDown = false;
        }

    }

}