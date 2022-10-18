using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    [RequireComponent(typeof(LineRenderer))]
    public class TouchRayVisualizer : MonoBehaviour
    {

        [SerializeField] private float drawDistance = 1f;
        [SerializeField] private float initialOffsetDistance = 0.1f;
        [SerializeField] private Transform anchorTransform;
        [SerializeField] private LayerMask layerMask;

        private LineRenderer linePointer;
        private LineRenderer LinePointer => linePointer ?? GetComponent<LineRenderer>();

        private void Update()
        {
            LinePointer.enabled = (OVRInput.GetActiveController() == OVRInput.Controller.Touch);
            Ray ray = new Ray(anchorTransform.position, anchorTransform.forward);
            LinePointer.SetPosition(0, ray.origin + ray.direction * initialOffsetDistance);
            float currentDrawDistance = CheckContacts();
            LinePointer.SetPosition(1, ray.origin + ray.direction * (currentDrawDistance - initialOffsetDistance));
        }

        // TODO: Reusing the PointerInteractorView collision check
        private float CheckContacts()
        {
            float result = drawDistance;
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction, Color.green, Time.deltaTime);
            RaycastHit[] hits = Physics.RaycastAll(ray, drawDistance, layerMask);
            foreach (RaycastHit hit in hits)
            {
                float distance = Vector3.Distance(anchorTransform.position, hit.point);
                if (distance < result)
                    result = distance;
            }
            return result;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

    }

}

