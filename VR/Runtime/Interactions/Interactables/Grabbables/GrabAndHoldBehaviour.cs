using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Assertions;
using Meta.Global;

namespace Meta.VR
{

    public class GrabAndHoldBehaviour : GrabbableBehaviour
    {

        [Header("GrabbableBehaviour")]
        [SerializeField] protected float animationSecs = 0.2f;
        [SerializeField] protected List<HandOffsetPair> offsets;

        private Transform initialParent;
        private Tweener translation;
        private Tweener rotation;

        protected override void Awake()
        {
            base.Awake();
            initialParent = transform.parent;
        }

        protected override void OnSecondaryClick(InteractorBehaviour interactor)
        {
            base.OnSecondaryClick(interactor);
            transform.SetParent(interactor.transform, true);
            Pose pose = GetOffsetPose(interactor);
            translation = transform.DOLocalMove(pose.position, animationSecs)
                .SetEase(Ease.Linear);
            rotation = transform.DOLocalRotate(pose.rotation.eulerAngles, animationSecs)
                .SetEase(Ease.Linear);
        }

        private Pose GetOffsetPose(InteractorBehaviour interactor)
        {
            Pose pose = new Pose();
            if (offsets.Count > 0)
            {
                Transform offset = offsets.GetOffsetByHand(interactor.hand);
                if (offset != null)
                    pose = offset.ToLocalPose();
            }
            return pose;
        }

        protected override void OnSecondaryUnclick(InteractorBehaviour interactor)
        {
            translation.Kill();
            rotation.Kill();
            transform.SetParent(initialParent, true);
            base.OnSecondaryUnclick(interactor);
        }

    }

    [Serializable]
    public class HandOffsetPair
    {
        public Hand hand;
        public Transform offset;
    }

    public static class HandOffsetPairExtensions
    {

        public static Transform GetOffsetByHand(this List<HandOffsetPair> pairs, Hand hand)
        {
            Assert.AreNotEqual(pairs.Count, 0, "There must be at least a pair");
            HandOffsetPair selection = null;
            pairs.ForEach(pair =>
            {
                if (pair.hand == hand)
                    selection = pair;
            });
            return selection.offset;
        }

    }

}