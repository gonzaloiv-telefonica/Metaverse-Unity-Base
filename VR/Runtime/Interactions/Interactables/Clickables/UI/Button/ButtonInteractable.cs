using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.VR
{

    [RequireComponent(typeof(Button))]
    public class ButtonInteractable : BaseClickable
    {

        private Button button;
        private Button Button => button ??= GetComponent<Button>();

        protected override void OnPrimaryClick()
        {
            Button.onClick?.Invoke();
        }

    }

}