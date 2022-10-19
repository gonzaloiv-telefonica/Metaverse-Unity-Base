using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using Meta.DataAccess;
using RSG;

namespace Meta.ShopifyStore
{

    public class ProductsPresenterTests : MonoBehaviour
    {

        private IService<Product> service;
        private ProductsPresenter presenter;
        private List<Product> products;

        [SetUp]
        public void TestSetup()
        {
            products = new List<Product>();
            products.Add(new Product());
            products.Add(new Product());
            Promise<List<Product>> promise = new Promise<List<Product>>();
            promise.Resolve(products);
            service = Substitute.For<IService<Product>>();
            service.Get(Arg.Any<Parameters>()).Returns(promise);
            presenter = new ProductsPresenter(service, null);
        }

        [Test]
        public void GetProductsReturnsAListOfProducts()
        {
            presenter.GetProducts()
                .Then(result => Assert.AreEqual(result, products));
        }

    }

}
