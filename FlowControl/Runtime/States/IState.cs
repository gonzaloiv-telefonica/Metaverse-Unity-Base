using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.FlowControl
{

    public interface IState
    {
        string Route { get; }
        void Enter();
        void Exit();
    }

}