using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    public abstract class Locator
    {

        protected Dictionary<Type, object> elements = new Dictionary<Type, object>();

        public void Register<T>(object instance)
        {
            elements[typeof(T)] = instance;
        }

        public void Unregister<T>(object instance)
        {
            if (Contains<T>())
            {
                elements.Remove(typeof(T));
            }
            else
            {
                Debug.LogWarning("Instance for type " + typeof(T) + " not found!");
            }
        }

        public bool Contains<T>()
        {
            return elements.ContainsKey(typeof(T));
        }

        public object GetElement<T>()
        {
            if (Contains<T>())
            {
                return elements[typeof(T)];
            }
            else
            {
                Debug.LogWarning("Client for type " + typeof(T) + " not found!");
                return null;
            }
        }

    }

}