using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

namespace Meta.Panels
{

    public class RayBehaviour : RayTool
    {

        public Interactable CurrentInteractable => _currInteractableCastedAgainst;
        public bool HasCurrentInteractable => CurrentInteractable != null;
        public bool IsPinching => ToolInputState == ToolInputState.PrimaryInputDownStay || ToolInputState == ToolInputState.PrimaryInputDown;
        public bool IsGrabbing => HasCurrentInteractable && IsPinching;

        public Vector3 CurrentCollisionPosition
        {
            get
            {
                RaycastHit[] hits = new RaycastHit[1];
                // ! Magic number originaly in OculusSampleFramework.RayTool
                Vector3 rayOrigin = transform.position + 0.8f * transform.forward;
                Vector3 rayDirection = transform.forward;
                Physics.RaycastNonAlloc(new Ray(rayOrigin, rayDirection), hits, Mathf.Infinity);
                if (hits != null && hits.Length > 0)
                    return hits[0].point;
                return transform.position;
            }
        }

    }

}

