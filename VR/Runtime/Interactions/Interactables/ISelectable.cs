using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public interface ISelectable
    {
        bool IsEnabled { get; }
        bool CanInteract(InteractorBehaviour interactor, RaycastHit hit);
        void AddInteractor(InteractorBehaviour interactor);
        void RemoveInteractor(InteractorBehaviour interactor);
    }

    public interface IRaySelectable : ISelectable { }

    public interface ISphereSelectable : ISelectable { }

    public interface ISelectableZone : ISelectable { }

}