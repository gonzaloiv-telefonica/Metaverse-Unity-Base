using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public abstract class BaseInteractable : MonoBehaviour
    {

        [Header("BaseInteractable")]
        [SerializeField] protected InteractableState state = InteractableState.Enabled;
        [SerializeField] protected float minDistance = 1f;

        protected List<InteractorBehaviour> interactors = new List<InteractorBehaviour>();
        protected IInteractableView view;

        public bool IsEnabled => state > InteractableState.Disabled;
        public bool HasInteractors => interactors != null && interactors.Count > 0;
        private IInteractableView View => view ??= GetComponent<IInteractableView>();

        protected virtual void Awake()
        {
            SetViewState(state);
        }

        public void SetState(InteractableState state)
        {
            this.state = state;
            SetViewState(state);
        }

        protected virtual void SetViewState(InteractableState state)
        {
            if (View != null && !View.Equals(null)) // https://gamedev.stackexchange.com/questions/128971/unity-c-interface-object-never-equals-null
                View.SetState(state);
        }

        public virtual void Disable()
        {
            SetState(InteractableState.Disabled);
            ClearInteractors();
        }

        protected virtual void ClearInteractors()
        {
            if (!HasInteractors)
                return;
            interactors.ForEach(interactor => RemoveInteractorListeners(interactor));
            interactors.Clear();
        }

        public virtual bool CanInteract(InteractorBehaviour interactor, RaycastHit hit)
        {
            return (!HasInteractors || !interactors.Contains(interactor)) && hit.distance <= minDistance;
        }

        public void AddInteractor(InteractorBehaviour interactor)
        {
            if (state == InteractableState.Disabled || interactors.Contains(interactor))
                return;
            interactors.Add(interactor);
            SetInteractorListeners(interactor);
            SetState(InteractableState.Hover);
        }

        public virtual void RemoveInteractor(InteractorBehaviour interactor)
        {
            if (!IsEnabled || !HasInteractors)
                return;
            RemoveInteractorListeners(interactor);
            interactors.Remove(interactor);
            SetState(InteractableState.Enabled);
        }

        protected abstract void SetInteractorListeners(InteractorBehaviour interactor);

        protected abstract void RemoveInteractorListeners(InteractorBehaviour interactor);

        protected virtual void OnDestroy()
        {
            if (!HasInteractors)
                return;
            interactors.ForEach(interactor => RemoveInteractorListeners(interactor));
        }

    }

    public enum InteractableState
    {
        Disabled,
        Enabled,
        Hover,
        Click
    }

}