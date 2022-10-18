using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Meta.Global;

namespace Meta.Audio
{

    public class PanelViewSfx : BaseView
    {

        [SerializeField] private ClipData showClipData;
        [SerializeField] private ClipData hideClipData;

        public override void Show()
        {
            SfxPlayer.Play(showClipData);
        }

        public override void Hide()
        {
            if(hideClipData.clip != null)
                SfxPlayer.Play(hideClipData);
        }

    }

}