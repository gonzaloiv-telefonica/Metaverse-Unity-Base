using System.Collections;
using System.Collections.Generic;
using RSG;
using UnityEngine;

namespace Meta.DataAccess
{

    public interface IService<T>
    {
        Promise<List<T>> Get();
        Promise<List<T>> Get(Parameters parameters);
    }

}
