using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;
using Meta.DataAccess;
using Meta.FileAccess;

namespace Meta.ShopifyStore
{

    public class ProductsPresenter
    {

        private IService<Product> service;
        private RemoteFileClient fileClient;

        public ProductsPresenter(IService<Product> service, RemoteFileClient fileClient)
        {
            this.service = service;
            this.fileClient = fileClient;
        }

        public Promise<List<Product>> GetProducts()
        {
            Parameters parameters = new ParametersBuilder()
                .AddLimit(3) // ? Hardcoded temporarily
                .Build();
            return service.Get(parameters);
        }

        public Promise<Texture> GetProductImage(Product product)
        {
            return (Promise<Texture>)fileClient.GetTexture(product.media.images[0].url);
        }

        public Promise<GameObject> GetProductModel(Product product)
        {
            // ? Just one glb at the moment for testing
            return fileClient.GetGlb(product.media.glbs[0].url);
        }

    }

}