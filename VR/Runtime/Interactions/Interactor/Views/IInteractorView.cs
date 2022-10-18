using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public interface IInteractorView
    {
        void ShowForInteractable(RaycastHit hit);
        void ShowForZone(RaycastHit hit);
        void Hide();
        void SetVisibility(bool isActive);
    }

}