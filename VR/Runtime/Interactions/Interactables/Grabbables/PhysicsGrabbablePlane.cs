using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Global;

namespace Meta.VR
{
    public class PhysicsGrabbablePlane : PhysicsGrabbable
    {
        protected override void OnSecondaryUnclick(InteractorBehaviour interactor)
        {
            base.OnSecondaryUnclick(interactor);
            GetComponent<Rigidbody>().AddForce(this.transform.forward * 5);
        }
    }
}