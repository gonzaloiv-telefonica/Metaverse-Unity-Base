using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.FlowControl
{

    public class Router
    {

        public static Router Instance => instance == null ? instance = new Router() : instance;
        private static Router instance;

        private StateMachine stateMachine;

        public static void Init(StateMachine stateMachine)
        {
            Instance.stateMachine = stateMachine;
        }

        public static void SetCurrentState(string route)
        {
            Instance.stateMachine.SetCurrentState(route);
        }

        public static void SetCurrentState<T>() where T : State
        {
            Instance.stateMachine.SetCurrentState<T>();
        }

        public static void Return()
        {
            Instance.stateMachine.Return();
        }

        public static bool IsCurrentState(string route)
        {
            return Instance.stateMachine.IsCurrentState(route);
        }

        public static string CurrentState()
        {
            return Instance.stateMachine.CurrentState();
        }

    }

}