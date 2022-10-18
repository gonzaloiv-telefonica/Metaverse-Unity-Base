using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public class HideRenderersOnAvatarCollision : MonoBehaviour
    {

        [SerializeField] private List<Renderer> rends;

        private void OnEnable()
        {
            rends.ForEach(rend => rend.enabled = true);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponentInParent<AvatarController>())
                rends.ForEach(rend => rend.enabled = false);
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.GetComponentInParent<AvatarController>())
                rends.ForEach(rend => rend.enabled = true);
        }

    }

}