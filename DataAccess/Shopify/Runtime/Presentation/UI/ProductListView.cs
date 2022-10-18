using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Meta.Global;
using UnityEngine.UI;
using Meta.FlowControl;

namespace Meta.ShopifyStore
{

    public class ProductListView : BaseView, IView
    {

        [SerializeField] private List<ProductView> productViews;
        [SerializeField] private List<Product> products;
        [SerializeField] private Button returnButton;

        private ProductsPresenter presenter;

        public virtual void Init(ProductsPresenter presenter)
        {
            Assert.AreNotEqual(productViews.Count, 0, "You must reference a Product View in the Editor");
            this.presenter = presenter;
            this.productViews[0].Init(presenter);
            returnButton.onClick.AddListener(() => Router.Return());
            Hide();
        }

        public override void Show()
        {
            base.Show();
            presenter.GetProducts()
                .Then(products =>
                {
                    this.products = products;
                    ShowPanels(products);
                })
                .Catch(Debug.LogException);
        }

        private void ShowPanels(List<Product> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (i >= productViews.Count)
                    InstantiateProductView();
                productViews[i].Show(products[i]);
            }
        }

        private void InstantiateProductView()
        {
            ProductView productView = Instantiate(productViews[0], productViews[0].transform.parent);
            productView.Init(presenter);
            productViews.Add(productView);
        }

        public override void Hide()
        {
            foreach (ProductView view in productViews)
            {
                view.Hide();
            }
        }

    }

}