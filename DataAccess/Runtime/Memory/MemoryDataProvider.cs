using System.Collections;
using System.Collections.Generic;
using RSG;
using UnityEngine;
using System;

namespace Meta.DataAccess
{

    /// <summary>
    /// Adapts MemoryDataClient to IDataProvider interface
    /// </summary>
    public class MemoryDataProvider : IDataProvider
    {

        private MemoryDataClient client;

        public MemoryDataProvider(MemoryDataClient client)
        {
            this.client = client;
        }

        public Promise<List<T>> Get<T>() where T : IEntity
        {
            Promise<List<T>> promise = new Promise<List<T>>();
            List<T> values = client.Get<T>();
            if (values == null)
            {
                promise.Reject(new Exception($"Values for type {typeof(T)} not found!"));
            }
            else
            {
                promise.Resolve(values);
            }
            return promise;
        }

        public Promise Put<T>(List<T> entities) where T : IEntity
        {
            Promise promise = new Promise();
            client.Put(entities);
            promise.Resolve();
            return promise;
        }

    }

}