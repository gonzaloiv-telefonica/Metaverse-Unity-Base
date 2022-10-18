using System;
using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using RSG;
using UnityEngine;

namespace Meta.DataAccess
{

    public interface IRestClient
    {
        Promise<List<T>> Get<T, A>(string uri) where T : IEntity, new() where A : IApiEntity<T>, new();
    }

}