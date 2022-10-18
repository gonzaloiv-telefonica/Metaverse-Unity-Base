
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Panels
{
    public class InstantFollowerPanel : MonoBehaviour
    {
        [SerializeField] private Transform targetToFollow;   
        [SerializeField] private float distanceToRaycastHit = 2f;
        [SerializeField] private float offset = 1.5f;
        [SerializeField] private float smoothTime = 0.3F;
        [SerializeField] private bool autoScale = true;
        [SerializeField] private LayerMask wallLayer;

        private float originalOffset = 1.5f;
        private Vector3 velocity = Vector3.zero;
        private RectTransform panelRectTransform;
        private Vector3 originalLocalScale;

        private void Start()
        {
            targetToFollow = Camera.main.transform;
            originalOffset = offset;
            panelRectTransform = GetComponent<RectTransform>();
            originalLocalScale = panelRectTransform.localScale;
        }
        public void LateUpdate()
        {
            if (Physics.Raycast(transform.position, targetToFollow.position, out RaycastHit hit, distanceToRaycastHit, wallLayer))
            {
                if (hit.distance < originalOffset)
                    offset = hit.distance * 0.9f;
            }
            else
            {
                offset = originalOffset;
            }

            if (autoScale)
                panelRectTransform.localScale = originalLocalScale * (offset / originalOffset);
                
            Vector3.SmoothDamp(transform.position, targetToFollow.position + targetToFollow.forward * offset, ref velocity, smoothTime);
            transform.rotation = new Quaternion(0.0f, targetToFollow.rotation.y, 0.0f, targetToFollow.rotation.w);

        }
    }

}
