using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Global;

namespace Meta.FlowControl
{

    [Serializable]
    public class State : IState
    {

        public Action onEnter;
        public Action onExit;

        protected List<IView> views;

        public string Route { get; set; }

        public State()
        {
            this.Route = this.GetType().Name;
            this.views = new List<IView>();
        }

        public State(params IView[] views) : this()
        {
            SetViews(views);
        }

        public void SetViews(params IView[] views)
        {
            foreach (IView view in views)
            {
                this.views.Add(view);
            }
        }

        public virtual void Enter()
        {
            onEnter?.Invoke();
            foreach (IView view in views)
            {
                view.Show();
            }
        }

        public virtual void Exit()
        {
            onExit?.Invoke();
            foreach (IView view in views)
            {
                view.Hide();
            }
        }

    }

}