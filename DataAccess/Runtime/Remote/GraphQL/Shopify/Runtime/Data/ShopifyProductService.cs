using System.Collections;
using System.Collections.Generic;
using RSG;
using UnityEngine;
using SimpleGraphQL;
using Newtonsoft.Json.Linq;
using Meta.DataAccess;

namespace Meta.ShopifyStore
{

    public class ShopifyProductService : IService<Product>
    {

        private GraphQLClient client;
        private GraphQLConfig config;

        public ShopifyProductService(GraphQLClient client, GraphQLConfig config)
        {
            this.client = client;
            this.config = config;
        }

        public Promise<List<Product>> Get()
        {
            Promise<List<Product>> promise = new Promise<List<Product>>();
            GetProducts(promise, ShopifyParametersParser.Default);
            return promise;
        }

        public Promise<List<Product>> Get(Parameters parameters)
        {
            Promise<List<Product>> promise = new Promise<List<Product>>();
            GetProducts(promise, ShopifyParametersParser.Parse(parameters));
            return promise;
        }

        public async void GetProducts(Promise<List<Product>> promise, Dictionary<string, object> parameters)
        {
            Query query = client.FindQuery("Product", "GetProducts", OperationType.Query);
            Request request = parameters != null ? query.ToRequest(parameters) : query.ToRequest();
            string results = await client.Send(request);
            if (string.IsNullOrEmpty(results))
            {
                promise.Reject(new System.Exception("This request had null result!"));
            }
            else
            {
                promise.Resolve(ParseProducts(results));
            }
        }

        private List<Product> ParseProducts(string results)
        {
            JObject response = JObject.Parse(results);
            JArray objs = (JArray)response["data"]["products"]["edges"];
            List<Product> entities = new List<Product>();
            foreach (JObject obj in objs)
            {
                entities.Add(obj.ToObject<ShopifyProduct>().ToEntity());
            }
            return entities;
        }

    }

}