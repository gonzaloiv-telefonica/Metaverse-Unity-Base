using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Meta.Global;
using MEC;
using Meta.Audio;
using System;
using UnityEngine.Assertions;

namespace Meta.VR
{

    public class ColorTintSelectableView : BaseUIInteractableView
    {

        private Selectable selectable;
        private Selectable Selectable => selectable ??= GetComponent<Selectable>();

        protected override void OnClick()
        {
            Selectable.targetGraphic.color = Selectable.colors.selectedColor;
        }

        protected override void OnHover()
        {
            Selectable.targetGraphic.color = Selectable.colors.selectedColor;
        }

        protected override void OnDisabled()
        {
            Selectable.targetGraphic.color = Selectable.colors.disabledColor;
        }

        protected override void OnEnabled()
        {
            Selectable.targetGraphic.color = Selectable.colors.normalColor;
        }


    }

}