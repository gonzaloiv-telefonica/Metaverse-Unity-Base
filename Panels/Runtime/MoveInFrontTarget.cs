using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Panels
{

    public class MoveInFrontTarget : MonoBehaviour
    {

        [SerializeField] private Transform targetToFollow;

        private void Start()
        {
            targetToFollow = Camera.main.transform;

        }

        public void ShowPanel()
        {
            if (targetToFollow == null)
                targetToFollow = Camera.main.transform;
            transform.position = targetToFollow.position;//Vector3.SmoothDamp(transform.position, targetToFollow.position + targetToFollow.forward * offset, ref velocity, smoothTime);
            transform.rotation = new Quaternion(0.0f, targetToFollow.rotation.y, 0.0f, targetToFollow.rotation.w);

        }

    }

}