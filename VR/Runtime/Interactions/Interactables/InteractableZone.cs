using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    [RequireComponent(typeof(Collider))]
    public class InteractableZone : MonoBehaviour, ISelectableZone
    {

        [SerializeField] protected float minDistance = 1f;

        public bool IsEnabled => throw new System.NotImplementedException();

        public bool CanInteract(InteractorBehaviour interactor, RaycastHit hit)
        {
            return hit.distance <= minDistance;
        }

        public void AddInteractor(InteractorBehaviour interactor) { }

        public void RemoveInteractor(InteractorBehaviour interactor) { }

    }

}