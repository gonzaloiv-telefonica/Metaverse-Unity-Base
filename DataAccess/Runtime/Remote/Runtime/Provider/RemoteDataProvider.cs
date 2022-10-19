using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;
using System;

namespace Meta.DataAccess
{

    public class RemoteDataProvider : Locator, IDataProvider
    {

        public virtual void Register<T>(IService<T> service)
        {
            base.Register<T>(service);
        }

        public virtual Promise<List<T>> Get<T>() where T : IEntity
        {
            return For<T>().Get();
        }

        public virtual Promise Put<T>(List<T> entities) where T : IEntity
        {
            throw new NotImplementedException();
        }

        protected virtual IService<T> For<T>()
        {
            return GetElement<T>() as IService<T>;
        }

    }

}