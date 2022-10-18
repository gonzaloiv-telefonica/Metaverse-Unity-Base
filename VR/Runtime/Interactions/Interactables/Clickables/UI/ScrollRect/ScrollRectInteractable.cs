using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.VR
{

    [RequireComponent(typeof(Collider))]
    public class ScrollRectInteractable : BaseClickable
    {

        [SerializeField] protected ScrollRect scrollRect;
        [SerializeField] protected Collider col;

        protected RaycastHit currentHit;
        protected RaycastHit initialHit;

        private bool isClicking;

        private bool IsValidDrag => isClicking && state != InteractableState.Disabled && HasInteractors && currentHit.collider != null;

        protected override void SetInteractorListeners(InteractorBehaviour interactor)
        {
            interactor.onPrimaryClick += OnPrimaryClick;
            interactor.onPrimaryUnclick += OnPrimaryUnclick;
        }

        protected override void OnPrimaryClick()
        {
            SetState(InteractableState.Click);
            initialHit = currentHit;
            isClicking = true;
        }

        protected virtual void OnPrimaryUnclick()
        {
            isClicking = false;
        }

        protected override void RemoveInteractorListeners(InteractorBehaviour interactor)
        {
            interactor.onPrimaryClick -= OnPrimaryClick;
            interactor.onPrimaryUnclick -= OnPrimaryUnclick;
        }

        public override bool CanInteract(InteractorBehaviour interactor, RaycastHit hit)
        {
            this.currentHit = hit;
            return base.CanInteract(interactor, hit);
        }

        private void Update()
        {
            CheckThumbstick();
            if (!IsValidDrag)
                return;
            SetYPosition();
        }

        protected virtual void SetYPosition()
        {
            Vector3 drag = currentHit.point - initialHit.point;
            drag *= scrollRect.scrollSensitivity;
            scrollRect.verticalNormalizedPosition -= drag.y;
        }

        private void CheckThumbstick()
        {
            Vector2 input = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            if (input.Equals(Vector2.zero))
                return;
            scrollRect.verticalNormalizedPosition += scrollRect.scrollSensitivity * input.y * Time.deltaTime;
        }

        public override void RemoveInteractor(InteractorBehaviour interactor)
        {
            base.RemoveInteractor(interactor);
            OnPrimaryUnclick();
        }

    }

}

