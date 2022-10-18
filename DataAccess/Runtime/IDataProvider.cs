using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;

namespace Meta.DataAccess
{

    public interface IDataProvider
    {
        Promise<List<T>> Get<T>() where T : IEntity;
        Promise Put<T>(List<T> entities) where T : IEntity;
    }

}