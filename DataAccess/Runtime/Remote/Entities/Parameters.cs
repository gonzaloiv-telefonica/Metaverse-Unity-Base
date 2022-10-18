using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    public class Parameters
    {
        public int limit;
        public bool HasLimit => limit != 0;
    }

}