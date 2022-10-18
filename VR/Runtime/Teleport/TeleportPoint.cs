using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public class TeleportPoint : ReachablePoint
    {

        [SerializeField] private Transform endPoint;
        public Transform EndPoint => endPoint;

    }

}