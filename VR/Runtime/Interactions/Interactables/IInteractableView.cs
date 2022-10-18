using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public interface IInteractableView
    {
        void SetState(InteractableState state);
    }

}