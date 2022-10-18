using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Global;

namespace Meta.VR
{

    public class PhysicsGrabbable : GrabAndHoldBehaviour
    {

        private Rigidbody rb;
        private Collider col;

        private Rigidbody Rb => rb ??= GetComponent<Rigidbody>();
        private Collider Col => col ??= GetComponentInChildren<Collider>();

        protected override void OnSecondaryClick(InteractorBehaviour interactor)
        {
            base.OnSecondaryClick(interactor);
            Rb.isKinematic = true;
            Col.isTrigger = true;
        }

        protected override void OnSecondaryUnclick(InteractorBehaviour interactor)
        {
            Rb.isKinematic = false;
            Col.isTrigger = false;
            rb.Apply(currentInteractor.GetRbVelocity());
            base.OnSecondaryUnclick(interactor);
        }

    }

}