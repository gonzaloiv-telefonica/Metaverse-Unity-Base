using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Audio;

namespace Meta.VR
{

    public class PistolBehaviour : TriggerableBehaviour
    {

        [Header("PistolBehaviour")]
        [SerializeField] private BufferedAudioSource audioSource;
        [SerializeField] private AudioClip shootClip;

        protected override void OnPrimaryClick()
        {
            base.OnPrimaryClick();
            audioSource.Play(shootClip);
            if (currentInteractor != null)
                VRHaptics.Vibrate(currentInteractor.hand.ToOVRController(), 0.2f);
        }

    }

}