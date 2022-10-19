using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Meta.VR
{

    public class HoldeableBehaviour : GrabbableBehaviour
    {

        [Header("GrabbableBehaviour")]
        [SerializeField] protected float mps = 2f;
        [SerializeField] protected float offsetRatio = 0.5f;

        private Tweener translation;

        protected override void OnSecondaryClick(InteractorBehaviour interactor)
        {
            if (translation.IsActive() && translation.IsPlaying())
                return;
            base.OnSecondaryClick(interactor);
            AvatarController.SetPlayerControllerActive(false);
            Vector3 destinationPosition = GetDestinationPosition();
            translation = AvatarController.Body.DOMove(destinationPosition, GetSecs(destinationPosition))
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => AvatarController.SetPlayerControllerActive(true));
        }

        private Vector3 GetDestinationPosition()
        {
            Vector3 destination = transform.position;
            Vector3 offset = (transform.position - AvatarController.CurrentPosition).normalized;
            destination -= new Vector3(offset.x * offsetRatio, 0, offset.y * offsetRatio);
            return destination;
        }   

        private float GetSecs(Vector3 destinationPosition)
        {
            return 1 / Vector3.Distance(AvatarController.CurrentPosition, destinationPosition) * mps;
        }

        protected override void OnSecondaryUnclick(InteractorBehaviour interactor)
        {
            translation.Kill();
            AvatarController.SetPlayerControllerActive(true);
            base.OnSecondaryUnclick(interactor);
        }

    }

}