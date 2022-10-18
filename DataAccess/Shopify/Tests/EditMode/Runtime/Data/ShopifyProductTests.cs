using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Meta.ShopifyStore
{

    public class ShopifyProductTests
    {

        [TestCase("Id", "Name")]
        [TestCase("", null)]
        public void ToEntityReturnsValidEntityData(string id, string name)
        {
            ShopifyProduct shopifyProduct = new ShopifyProduct();
            shopifyProduct.Node.Id = id;
            shopifyProduct.Node.Title = name;
            Product product = shopifyProduct.ToEntity();
            Assert.AreEqual(id, product.id, "Incorrect info was returned for product");
            Assert.AreEqual(name, product.name, "Incorrect info was returned for product");
        }

    }

}