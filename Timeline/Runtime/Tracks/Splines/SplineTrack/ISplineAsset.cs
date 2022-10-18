using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Timeline
{

    interface ISplineAsset
    {
        double ClipStart
        {
            set;
            get;
        }

        double RealDuration
        {
            set;
            get;
        }
    }

}