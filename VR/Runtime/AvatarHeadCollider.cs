using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Meta.VR
{
    public class AvatarHeadCollider : MonoBehaviour
    {
        [SerializeField] Transform centerEyeAnchor;
        
        void Update()
        {
            transform.position = centerEyeAnchor.position;
        }
}
}