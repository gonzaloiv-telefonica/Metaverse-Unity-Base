using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

namespace Meta.VR
{

    public class ConstraintOVRGrabbable : OVRGrabbable
    {

        [SerializeField] private bool constraintXRotation;
        [SerializeField] private bool constraintZRotation;

        private Vector3 initialRotation;

        public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            base.GrabBegin(hand, grabPoint);
            initialRotation = transform.localRotation.eulerAngles;
        }

        public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(linearVelocity, angularVelocity);
            float xRotation = constraintXRotation ? initialRotation.x : transform.rotation.x;
            float zRotation = constraintZRotation ? initialRotation.z : transform.rotation.z;
            transform.localRotation = Quaternion.Euler(new Vector3(xRotation, transform.localRotation.eulerAngles.y, zRotation));
        }

    }

}