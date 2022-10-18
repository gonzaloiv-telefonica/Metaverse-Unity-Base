using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Meta.VR
{

    public class BodyInteractableView : MonoBehaviour, IInteractableView
    {

        // ! There must be a body for each InteractableState
        [SerializeField] private InteractableStateBodyPair[] bodyPairs;

        public virtual void SetState(InteractableState state)
        {
            HideBodies();
            bodyPairs.GetBodyByState(state).SetActive(true);
        }

        private void HideBodies()
        {
            foreach (InteractableStateBodyPair pair in bodyPairs)
            {
                pair.body.SetActive(false);
            }
        }

    }

    [Serializable]
    public class InteractableStateBodyPair
    {
        public InteractableState state;
        public GameObject body;
    }

    public static class InteractableBehaviourExtensions
    {

        public static GameObject GetBodyByState(this IEnumerable<InteractableStateBodyPair> pairs, InteractableState state)
        {
            foreach (InteractableStateBodyPair pair in pairs)
            {
                if (pair.state == state)
                    return pair.body;
            }
            return null;
        }

    }

}