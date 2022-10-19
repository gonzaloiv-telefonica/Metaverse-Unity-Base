using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public interface IPooleable
    {
        bool IsActive { get; }
        void Reset();
    }

}

