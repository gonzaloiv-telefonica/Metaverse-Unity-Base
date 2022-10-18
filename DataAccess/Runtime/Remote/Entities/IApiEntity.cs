using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    public interface IApiEntity<B> where B : new()
    {
        B ToEntity();
    }

}