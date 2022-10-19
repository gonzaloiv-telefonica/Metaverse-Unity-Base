using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Meta.FlowControl;

namespace Meta.ShopifyStore
{

    public class ProductSpawner : MonoBehaviour, IView
    {

        [SerializeField] private Transform spawnPoint;

        private ProductsPresenter presenter;
        private List<GameObject> gos;

        public void Init(ProductsPresenter presenter)
        {
            this.presenter = presenter;
            this.gos = new List<GameObject>();
        }

        public void Show()
        {
            presenter.GetProducts()
                .Then(products =>
                {
                    foreach (Product product in products)
                    {
                        SpawnProduct(product);
                    }
                })
                .Catch(Debug.LogException);
        }

        private void SpawnProduct(Product product)
        {
            if (!product.HasGLBs)
                return;
            presenter.GetProductModel(product)
                .Then(SetupGO)
                .Catch(Debug.LogException);
        }

        private void SetupGO(GameObject go)
        {
            go.transform.position = spawnPoint.position;
            go.transform.rotation = spawnPoint.rotation;
            go.transform.localScale = go.transform.localScale / 100;
            gos.Add(go);
        }

        public void Hide()
        {
            foreach (GameObject go in gos)
            {
                Destroy(go);
            }
        }

    }

}