using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MEC;

namespace Meta.VR
{

    public class InteractableBehaviour : BaseClickable
    {

        protected Action<InteractableBehaviour> onInteraction;

        public virtual void AddListener(Action<InteractableBehaviour> onInteraction)
        {
            this.onInteraction = onInteraction;
        }

        public virtual void RemoveListener()
        {
            onInteraction = null;
        }

        protected override void OnPrimaryClick()
        {
            onInteraction?.Invoke(this);
        }

    }

}