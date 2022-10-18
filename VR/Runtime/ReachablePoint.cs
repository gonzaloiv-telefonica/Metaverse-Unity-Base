using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Meta.VR;

namespace Meta.VR
{

    public class ReachablePoint : MonoBehaviour
    {

        public Action onAvatarEnter = () => { };
        public Action onAvatarExit = () => { };

        private MeshRenderer[] myMeshes;

        private void Start()
        {
            myMeshes = GetComponentsInChildren<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "PlayerHead")
                onAvatarEnter.Invoke();
        }
        private void OnTriggerExit(Collider col)
        {
            if (col.gameObject.tag == "PlayerHead")
                onAvatarExit.Invoke();
        }

        public void HideVisuals()
        {
            foreach (MeshRenderer mesh in myMeshes)
            {
                mesh.enabled = false;
            }
        }

        public void ShowVisuals()
        {
            foreach (MeshRenderer mesh in myMeshes)
            {
                mesh.enabled = true;
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

    }

}