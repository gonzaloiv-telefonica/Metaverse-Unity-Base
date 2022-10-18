using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public class ScrollbarInteractable : ScrollRectInteractable
    {

        // ? Scrollbar direction is the opposite to scroll rect drag
        protected override void SetYPosition()
        {
            float percentage = (currentHit.point.y - col.bounds.min.y) / (col.bounds.max.y - col.bounds.min.y);
            scrollRect.verticalNormalizedPosition = percentage;
        }

    }

}
