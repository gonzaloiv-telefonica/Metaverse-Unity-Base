using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    public interface IMemoryDataClient
    {
        public List<T> Get<T>() where T : IEntity;
        public void Put<T>(List<T> values) where T : IEntity;
        public T GetSingle<T>() where T : IEntity;
        public void PutSingle<T>(T value) where T : IEntity;
    }

}