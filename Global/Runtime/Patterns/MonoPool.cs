using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    [System.Serializable]
    public class MonoPool<T> where T : MonoBehaviour, IPooleable
    {

        [SerializeField] private List<T> instances;
        private Transform parent;

        public MonoPool(T instance)
        {
            instances = new List<T>();
            instances.Add(instance);
            parent = instance.transform.parent;
        }

        public T GetInactive()
        {
            T instance = null;
            for (int i = 0; i < instances.Count; i++)
            {
                if (!instances[i].IsActive)
                    instance = instances[i];
            }
            if (instance == null)
                instance = Instantiate();
            instance.Reset();
            return instance;
        }

        private T Instantiate()
        {
            T instance = GameObject.Instantiate(instances[0], parent)
                .GetComponent<T>();
            instances.Add(instance);
            return instance;
        }

    }

}