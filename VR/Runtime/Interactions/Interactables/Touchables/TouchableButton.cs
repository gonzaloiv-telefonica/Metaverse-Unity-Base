using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.VR
{

    public class TouchableButton : BaseTouchable
    {

        private Button button;
        private Button Button => button ??= GetComponent<Button>();

        protected override void OnTouch()
        {
            Button.onClick.Invoke();
        }
    }

}