using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Meta.Global
{

    public static class RbExtensions
    {

        public static RbVelocity ResetVelocity(this Rigidbody rb)
        {
            RbVelocity rbVelocity = new RbVelocity(rb.velocity, rb.angularVelocity);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return rbVelocity;
        }

        public static void Apply(this Rigidbody rb, RbVelocity rbVelocity)
        {
            rb.velocity = rbVelocity.linear;
            rb.angularVelocity = rbVelocity.angular;
        }

    }

    public class RbVelocity
    {

        public Vector3 linear;
        public Vector3 angular;

        public RbVelocity(Vector3 linear, Vector3 angular)
        {
            this.linear = linear;
            this.angular = angular;
        }

    }

}