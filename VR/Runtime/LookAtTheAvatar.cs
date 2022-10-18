using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.VR;

namespace Meta.MovistarExperience
{

    public class LookAtTheAvatar : MonoBehaviour
    {

        [SerializeField] private float minDistanceToLook = 20f;

        private float DistanceToAvatar => Vector3.Distance(AvatarController.CurrentPosition, transform.position);

        private void Update()
        {
            if (AvatarController.Instance == null)
                return;
            if (DistanceToAvatar > minDistanceToLook)
                return;
            transform.LookAt(AvatarController.CurrentPosition);
            // ? Fix found at: https://gamedev.stackexchange.com/questions/174759/unity-transform-lookattarget-not-looking-at-target
            transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);
        }

    }

}