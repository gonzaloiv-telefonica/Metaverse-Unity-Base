using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Meta.VR
{

    public class GrabbableBehaviour : BaseInteractable, IRaySelectable, ISphereSelectable
    {

        protected InteractorBehaviour currentInteractor;

        public Action onGrab = () => { };
        public Action onUngrab = () => { };

        protected override void SetInteractorListeners(InteractorBehaviour interactor)
        {
            interactor.onSecondaryClick += OnSecondaryClick;
        }

        protected override void RemoveInteractorListeners(InteractorBehaviour interactor)
        {
            interactor.onSecondaryClick -= OnSecondaryClick;
        }

        protected virtual void OnSecondaryClick(InteractorBehaviour interactor)
        {
            if (currentInteractor != null)
            {
                if (interactor == currentInteractor)
                    return;
                RemoveCurrentInteractorListeners();
            }
            currentInteractor = interactor;
            currentInteractor.onSecondaryUnclick += OnSecondaryUnclick;
            onGrab?.Invoke();
        }

        private void RemoveCurrentInteractorListeners()
        {
            currentInteractor.onSecondaryClick -= OnSecondaryClick;
            currentInteractor.onSecondaryUnclick -= OnSecondaryUnclick;
        }

        protected virtual void OnSecondaryUnclick(InteractorBehaviour interactor)
        {
            interactor.onSecondaryUnclick -= OnSecondaryUnclick;
            currentInteractor = null;
            onUngrab?.Invoke();
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (currentInteractor != null)
                RemoveCurrentInteractorListeners();
        }

    }

}