using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Meta.VR
{

    public class ShootableBehaviour : MonoBehaviour
    {

        public Action onShot = () => { };

        private void OnTriggerEnter(Collider col)
        {
            if (col.attachedRigidbody != null && col.attachedRigidbody.GetComponent<Bullet>())
                OnShot();
        }

        protected virtual void OnShot()
        {
            gameObject.SetActive(false);
            onShot?.Invoke();
        }

    }

}