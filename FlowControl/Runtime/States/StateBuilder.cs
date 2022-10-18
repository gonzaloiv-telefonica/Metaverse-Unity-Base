using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Meta.Global;

namespace Meta.FlowControl
{

    public class StateBuilder<T> where T : State, new()
    {

        protected T state;

        public StateBuilder()
        {
            state = new T();
        }

        public StateBuilder(T state)
        {
            this.state = state;
        }

        public StateBuilder<T> Route(string route)
        {
            state.Route = route;
            return this;
        }

        public StateBuilder<T> Enter(Action onEnter)
        {
            state.onEnter = onEnter;
            return this;
        }

        public StateBuilder<T> Exit(Action onExit)
        {
            state.onExit = onExit;
            return this;
        }

        public StateBuilder<T> Views(params IView[] views)
        {
            state.SetViews(views);
            return this;
        }

        public T Build()
        {
            return state;
        }

    }

}