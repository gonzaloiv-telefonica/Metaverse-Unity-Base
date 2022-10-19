using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public class Bullet : MonoBehaviour, IPooleable
    {

        [SerializeField] private float baseForce = 30;
        private Rigidbody rb;
        private Transform parent;

        public bool IsActive => gameObject.activeInHierarchy;
        private Rigidbody Rb => rb ??= GetComponent<Rigidbody>();
        private Transform Parent => parent ??= transform.parent;

        private void OnTriggerEnter(Collider col)
        {
            if (col.isTrigger)
                return;
            gameObject.SetActive(false);
            SetActive(false);
        }

        public void Shoot()
        {
            transform.parent = null;
            SetActive(true);
            Rb.isKinematic = false;
            Rb.AddForce(transform.forward * baseForce, ForceMode.Impulse);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void Reset()
        {
            transform.parent = Parent;
            Rb.isKinematic = true;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        
    }

}