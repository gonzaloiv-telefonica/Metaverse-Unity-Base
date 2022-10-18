using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;

namespace Meta.DataAccess
{

    public class DataLocator
    {

        private IDataProvider remoteDataProvider;
        private IDataProvider memoryDataProvider;

        public DataLocator(IDataProvider remoteDataProvider, IDataProvider memoryDataProvider)
        {
            this.remoteDataProvider = remoteDataProvider;
            this.memoryDataProvider = memoryDataProvider;
        }

        public Promise<List<T>> Get<T>() where T : IEntity
        {
            Promise<List<T>> result = new Promise<List<T>>();
            memoryDataProvider.Get<T>()
                .Then(entities =>
                {
                    Debug.Log("Memory load!");
                    result.Resolve(entities);
                })
                .Catch(ex =>
                {
                    remoteDataProvider.Get<T>()
                        .Then(entities =>
                        {
                            Debug.Log("Remote load!");
                            memoryDataProvider.Put(entities);
                            result.Resolve(entities);
                        })
                        .Catch(result.Reject);
                });
            return result;
        }

    }

}