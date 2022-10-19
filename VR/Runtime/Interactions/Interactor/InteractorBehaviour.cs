using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Global;
using System;

namespace Meta.VR
{

    public class InteractorBehaviour : MonoBehaviour
    {

        private const float rayDistance = 10f;
        private const float sphereDistance = 0.05f;

        [Header("Settings")]
        [SerializeField] public Hand hand;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private OVRInput.Button firstButtonCode = OVRInput.Button.PrimaryIndexTrigger;
        [SerializeField] public OVRInput.Button secondButtonCode = OVRInput.Button.SecondaryIndexTrigger;
        [SerializeField] private OVRInput.Controller controller;

        private ISelectable currentSelectable;
        private IInteractorView view;

        public Action onPrimaryClick = () => { };
        public Action onPrimaryUnclick = () => { };
        public Action<InteractorBehaviour> onSecondaryClick = (InteractorBehaviour interactorBehaviour) => { };
        public Action<InteractorBehaviour> onSecondaryUnclick = (InteractorBehaviour interactorBehaviour) => { };

        private IInteractorView View => view ??= GetComponent<IInteractorView>();

        private void Update()
        {
            CheckInput();
            Interaction interaction = CastRay();
            if (interaction == null)
                interaction = CastSphere();
            if (interaction == null)
                ResetCurrentSelectable();
            if (interaction != null)
                ProcessInteraction(interaction);
        }

        private void CheckInput()
        {
            if (OVRInput.GetDown(firstButtonCode) || Input.GetMouseButtonDown(0))
                onPrimaryClick.Invoke();
            if (OVRInput.GetUp(firstButtonCode) || Input.GetMouseButtonUp(0))
                onPrimaryUnclick.Invoke();
            if (OVRInput.GetDown(secondButtonCode) || Input.GetKeyDown(KeyCode.LeftAlt))
                onSecondaryClick.Invoke(this);
            if (OVRInput.GetUp(secondButtonCode) || Input.GetKeyUp(KeyCode.LeftAlt))
                onSecondaryUnclick.Invoke(this);
        }

        private Interaction CastRay()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance, layerMask);
            Interaction interaction = ProcessHits<IRaySelectable>(hits);
            if (interaction == null)
                CheckInteractableZones(hits);
            return interaction;
        }

        private Interaction ProcessHits<T>(RaycastHit[] hits) where T : ISelectable
        {
            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    ISelectable selectable = hit.transform.GetComponent<T>();
                    if (selectable == null || !selectable.IsEnabled)
                        continue;
                    return new Interaction(selectable, hit);
                }
            }
            return null;
        }

        private void ProcessInteraction(Interaction interaction)
        {
            if (!interaction.IsValidInteractor(this))
                return;
            View.ShowForInteractable(interaction.hit);
            if (currentSelectable != interaction.selectable && interaction.selectable.CanInteract(this, interaction.hit))
                SetSelectable(interaction.selectable);
        }

        private void SetSelectable(ISelectable selectable)
        {
            ResetCurrentSelectable();
            currentSelectable = selectable;
            currentSelectable.AddInteractor(this);
        }

        private void ResetCurrentSelectable()
        {
            if (currentSelectable != null)
            {
                currentSelectable.RemoveInteractor(this);
                currentSelectable = null;
            }
        }

        private void CheckInteractableZones(RaycastHit[] hits)
        {
            bool isInInteractableZone = ShowIsInInteractableZone(hits);
            if (!isInInteractableZone)
                View.Hide();
        }

        private bool ShowIsInInteractableZone(RaycastHit[] hits)
        {
            foreach (RaycastHit hit in hits)
            {
                ISelectableZone zone = hit.transform.GetComponent<ISelectableZone>();
                if (zone == null || !zone.CanInteract(this, hit))
                    continue;
                View.ShowForZone(hit);
                return true;
            }
            return false;
        }

        private Interaction CastSphere()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereDistance, transform.forward, sphereDistance, layerMask);
            return ProcessHits<ISphereSelectable>(hits);
        }

        public virtual void SetVisibility(bool isVisible)
        {
            View.SetVisibility(isVisible);
        }

        public virtual RbVelocity GetRbVelocity()
        {
            OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(controller), orientation = OVRInput.GetLocalControllerRotation(controller) };
            OVRPose offsetPose = new OVRPose { position = Vector3.zero, orientation = Quaternion.identity };
            localPose *= offsetPose;
            OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
            Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(controller);
            Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(controller);
            return new RbVelocity(linearVelocity, angularVelocity);
        }

    }

    public enum Hand
    {
        Left,
        Right
    }

    public static class InteractorBehaviourExtensions
    {

        public static bool AreEqualsTo(this List<InteractorBehaviour> ours, List<InteractorBehaviour> theirs)
        {
            if (ours == null && theirs != null)
                return false;
            if (ours != null && theirs == null)
                return false;
            if (ours.Count != theirs.Count)
                return false;
            foreach (InteractorBehaviour interactor in ours)
            {
                if (!theirs.Contains(interactor))
                    return false;
            }
            return true;
        }

        public static List<InteractorBehaviour> Clone(this List<InteractorBehaviour> interactors)
        {
            List<InteractorBehaviour> result = new List<InteractorBehaviour>();
            interactors.ForEach(interactor => result.Add(interactor));
            return result;
        }

        public static OVRInput.Controller ToOVRController(this Hand hand)
        {
            return hand switch
            {
                Hand.Left => OVRInput.Controller.LTouch,
                Hand.Right => OVRInput.Controller.RTouch,
                _ => OVRInput.Controller.None,
            };
        }

    }

}