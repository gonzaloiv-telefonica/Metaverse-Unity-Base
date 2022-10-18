using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

namespace Meta.VR
{

    public class ScaleGOView : BaseUIInteractableView
    {

        [SerializeField] private Vector3 scaleTargetOnHover = Vector3.one;

        protected override void OnClick() { }

        protected override void OnDisabled()
        {
            gameObject.transform.localScale = Vector3.one;
        }

        protected override void OnEnabled()
        {
            gameObject.transform.localScale = Vector3.one;
        }

        protected override void OnHover()
        {
            gameObject.transform.localScale = scaleTargetOnHover;
        }

    }

}