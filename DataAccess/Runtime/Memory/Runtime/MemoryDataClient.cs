using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RSG;
using System.Linq;

namespace Meta.DataAccess
{

    public class MemoryDataClient : IMemoryDataClient
    {

        private Dictionary<Type, List<object>> pairs = new Dictionary<Type, List<object>>();

        public List<T> Get<T>() where T : IEntity
        {
            List<object> pair;
            pairs.TryGetValue(typeof(T), out pair);
            return pair == null ? null : pair.Cast<T>().ToList();
        }

        public void Put<T>(List<T> values) where T : IEntity
        {
            pairs[typeof(T)] = values.Cast<object>().ToList();
        }

        public T GetSingle<T>() where T : IEntity
        {
            List<object> pair;
            pairs.TryGetValue(typeof(T), out pair);
            return pair == null ? default(T) : pair.Cast<T>().ToList()[0];
        }

        public void PutSingle<T>(T value) where T : IEntity
        {
            pairs[typeof(T)] = new List<object> { value };
        }

    }

}