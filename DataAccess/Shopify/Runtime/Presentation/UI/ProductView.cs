using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Meta.Global;

namespace Meta.ShopifyStore
{

    public class ProductView : BaseView
    {

        [SerializeField] private UnityEngine.UI.Image image;
        [SerializeField] private TMP_Text nameLabel;

        private ProductsPresenter presenter;

        public void Init(ProductsPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Show(Product product)
        {
            base.Show();
            SetImageActive(false);
            nameLabel.text = product.name;
            if (product.HasImages)
                ShowImage(product);
        }

        private void ShowImage(Product product)
        {
            presenter.GetProductImage(product)
                .Then(texture =>
                {
                    image.sprite = texture.ToSprite();
                    SetImageActive(true);
                })
                .Catch(Debug.LogException);
        }

        private void SetImageActive(bool isActive)
        {
            image.gameObject.SetActive(isActive);
        }

    }

}