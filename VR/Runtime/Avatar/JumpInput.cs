using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public class JumpInput : MonoBehaviour
    {

        [SerializeField] private OVRInput.Button buttonCode = OVRInput.Button.SecondaryThumbstick;
        [SerializeField] private OVRPlayerController playerController;

        private void Update()
        {

            if (OVRInput.GetDown(buttonCode) || Input.GetKeyDown(KeyCode.Space))
                playerController.Jump();
        }

    }

}