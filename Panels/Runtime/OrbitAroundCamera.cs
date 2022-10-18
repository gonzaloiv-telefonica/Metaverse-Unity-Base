using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Panels
{

    public class OrbitAroundCamera : MonoBehaviour
    {

        private const float threshold = 15;

        [Range(0.1f, 5)]
        [SerializeField] private float speedMultiplier = 1;

        private Camera cam;
        private float distanceToCam;

        private void Awake()
        {
            cam = Camera.main;
            distanceToCam = Mathf.Log(Vector3.Distance(cam.transform.position, transform.position));
            distanceToCam = distanceToCam > 1 ? distanceToCam : 1; // ? Minimum value must be 1 
        }

        private void Update()
        {
            float angle = AngleAroundAxis(transform.position, cam.transform.forward, Vector3.up);
            if (Mathf.Abs(angle) > (threshold * distanceToCam))
                transform.RotateAround(cam.transform.position, Vector3.up, -angle * Time.deltaTime * speedMultiplier);
        }

        // Ref: https://mydevelopertidbits.blogspot.com/2021/01/calculate-angle-between-vectors-on.html
        private float AngleAroundAxis(Vector3 v, Vector3 forward, Vector3 perpenducularAxis)
        {
            Vector3 right = Vector3.Cross(perpenducularAxis, forward);
            forward = Vector3.Cross(right, perpenducularAxis);
            return Mathf.Atan2(Vector3.Dot(v, right), Vector3.Dot(v, forward)) * Mathf.Rad2Deg;
        }

    }

}