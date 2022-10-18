using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Global
{

    public abstract class BaseView : MonoBehaviour
    {

        [Header("BaseView")]
        [SerializeField] private List<BaseView> views;
        public bool IsActive => gameObject.activeInHierarchy;

        public virtual void Init()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            AddListeners();
            views.ForEach(view => view?.Show());
            gameObject.SetActive(true);
        }

        protected virtual void AddListeners() { }

        public virtual void Hide()
        {
            RemoveListeners();
            views.ForEach(view => view?.Hide());
            gameObject.SetActive(false);
        }

        protected virtual void RemoveListeners() { }

    }

}