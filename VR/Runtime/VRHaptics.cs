using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace Meta.VR
{

    public class VRHaptics
    {

        private static VRHaptics instance;
        private static VRHaptics Instance => instance ??= new VRHaptics();

        private int calls;
        private bool isPlaying;

        public static void Vibrate(OVRInput.Controller controller, float secs)
        {
            if (!Instance.isPlaying)
            {
                Timing.RunCoroutine(VibrationRoutine(controller, secs));
            }
            else
            {
                Instance.calls++;
            }
        }

        private static IEnumerator<float> VibrationRoutine(OVRInput.Controller controller, float secs)
        {
            Instance.isPlaying = true;
            OVRInput.SetControllerVibration(1f, 1f, controller);
            yield return Timing.WaitForSeconds(secs);
            Instance.isPlaying = false;
            if (Instance.calls != 0)
            {
                Instance.calls--;
                Timing.RunCoroutine(VibrationRoutine(controller, secs));
            }
            else
            {
                OVRInput.SetControllerVibration(0, 0, controller);
            }
        }

    }

}