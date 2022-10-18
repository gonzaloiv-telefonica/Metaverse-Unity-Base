using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.VR;

namespace Meta.MovistarExperience
{

    public class OutlineInteractableView : MonoBehaviour, IInteractableView
    {

        private Outline[] outlines;
        private Outline[] Outlines => outlines ??= GetComponentsInChildren<Outline>();

        private bool HasOutlines => Outlines != null;

        public void SetState(InteractableState state)
        {
            if (!HasOutlines)
                return;
            foreach (Outline outline in Outlines)
            {
                outline.enabled = state == InteractableState.Hover;
            }
        }

    }

}