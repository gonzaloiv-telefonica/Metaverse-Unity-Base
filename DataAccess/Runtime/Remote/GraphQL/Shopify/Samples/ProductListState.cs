using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.FlowControl;
using Meta.Global;
using UnityEngine.UI;

namespace Meta.ShopifyStore
{

    public class InitState : State
    {

        private Button toProductListButton;

        public InitState(Button toProductListButton) : base()
        {
            this.toProductListButton = toProductListButton;
        }

        public override void Enter()
        {
            toProductListButton.gameObject.SetActive(true);
            toProductListButton.onClick.AddListener(ToNextState);
        }

        public override void Exit()
        {
            toProductListButton.gameObject.SetActive(false);
            toProductListButton.onClick.RemoveListener(ToNextState);
        }

        private void ToNextState()
        {
            Router.SetCurrentState("ProductListState");
        }

    }

}