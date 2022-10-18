using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public class TinyHeadCollider : MonoBehaviour
    {

        [SerializeField] private LayerMask layerMask;
        private int touchingWalls;

        public bool IsTouchingWalls => touchingWalls > 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger)
                return;
            if (layerMask == (layerMask | (1 << other.gameObject.layer)))
                touchingWalls++;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.isTrigger)
                return;
            if (layerMask == (layerMask | (1 << other.gameObject.layer)))
                touchingWalls--;
        }

    }

}