using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Panels
{

    [CreateAssetMenu(fileName = "FlowData", menuName = "Meta/Panels/FlowData")]
    public class FlowData : ScriptableObject
    {
        public List<StateData> states;
    }

}