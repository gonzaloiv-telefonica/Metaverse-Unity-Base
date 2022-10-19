using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public class HideRenderersWhenNearAvatar : MonoBehaviour
    {

        [SerializeField] private float minDistance = 1;

        private Renderer[] rends;
        private Renderer[] Rends => rends ??= GetComponentsInChildren<Renderer>();

        private Outline outline;
        private Outline Outline => outline ??= GetComponent<Outline>();

        private Collider col;
        private Collider Col => col ??= GetComponent<Collider>();

        private void Update()
        {
            bool isInDistance = Vector3.Distance(AvatarController.CurrentPosition, transform.position) < minDistance;
            foreach (Renderer rend in Rends)
            {
                rend.enabled = !isInDistance;
            }
            Color color = new Color(Outline.OutlineColor.r, Outline.OutlineColor.g, Outline.OutlineColor.b, isInDistance ? 0 : 1);
            Outline.OutlineColor = color;
            Col.enabled = !isInDistance;
        }

    }

}