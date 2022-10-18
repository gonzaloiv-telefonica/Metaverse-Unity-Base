using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Meta.FlowControl
{

    /// <summary>
    /// Base on https://github.com/Real-Serious-Games/Fluent-State-Machine, and: https://bitbucket.org/jparham/blog-tic-tac-toe/src/master/Assets/Scripts/Common/StateMachine/
    /// </summary>
    [Serializable]
    public class StateMachine
    {

        private IState currentState;
        private Dictionary<string, IState> states;
        private Stack<IState> previousStates;

        public StateMachine()
        {
            this.states = new Dictionary<string, IState>();
            this.previousStates = new Stack<IState>();
        }

        public void Register(IState state)
        {
            if (!states.ContainsKey(state.Route))
                states[state.Route] = state;
        }

        private IState Get(string route)
        {
            IState state;
            states.TryGetValue(route, out state);
            return state;
        }


        public void SetCurrentState(string route)
        {
            ExitCurrentState();
            currentState = Get(route);
            if (currentState == null)
                throw new Exception($"State with route: {route} not found!");
            currentState.Enter();
        }

        private void ExitCurrentState()
        {
            if (currentState == null)
                return;
            currentState.Exit();
            previousStates.Push(currentState);
        }

        public void SetCurrentState<T>() where T : State
        {
            ExitCurrentState();
            states.TryGetValue(typeof(T).Name, out currentState);
            if (currentState == null)
                throw new Exception($"State of type {typeof(T)} not found!");
            currentState.Enter();
        }

        public void Return()
        {
            currentState.Exit();
            currentState = previousStates.Pop();
            currentState.Enter();
        }

        public bool IsCurrentState(string route)
        {
            return currentState != null && string.Equals(currentState.Route, route);
        }

        public string CurrentState()
        {
            return currentState.Route;
        }

    }

}