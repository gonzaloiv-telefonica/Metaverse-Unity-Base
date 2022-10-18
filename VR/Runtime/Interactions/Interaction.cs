using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    [System.Serializable]
    public class Interaction
    {

        public ISelectable selectable;
        public RaycastHit hit;

        public bool HasInteractable => selectable != null;

        public Interaction(ISelectable selectable, RaycastHit hit)
        {
            this.selectable = selectable;
            this.hit = hit;
        }

        public bool IsValidInteractor(InteractorBehaviour interactor)
        {
            return selectable.CanInteract(interactor, hit);
        }

    }

}