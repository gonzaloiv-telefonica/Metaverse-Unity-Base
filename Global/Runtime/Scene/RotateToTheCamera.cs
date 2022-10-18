using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Global
{

    public class RotateToTheCamera : MonoBehaviour
    {

        [SerializeField] private Vector3 upAxis = Vector3.up;

        private Camera Cam => cam ??= Camera.main;
        private Camera cam;

        private void Update()
        {
            Vector3 point = new Vector3(Cam.transform.position.x,
                                        transform.position.y,
                                        Cam.transform.position.z);
            transform.LookAt(point);
        }

    }

}