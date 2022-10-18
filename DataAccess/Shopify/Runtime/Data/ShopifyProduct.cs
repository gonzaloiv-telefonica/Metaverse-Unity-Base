using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Meta.DataAccess;

namespace Meta.ShopifyStore
{

    public class ShopifyProduct : IApiEntity<Product>
    {

        [JsonProperty("node")]
        public PurpleNode Node { get; set; }

        public ShopifyProduct()
        {
            Node = new PurpleNode();
            Node.Media = new ShopifyMedia();
        }

        public Product ToEntity()
        {
            Product product = new Product();
            product.id = Node.Id;
            product.name = Node.Title;
            product.media = Node.Media.ToEntity();
            return product;
        }

    }

    public partial class PurpleNode
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("handle")]
        public string Handle { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("media")]
        public ShopifyMedia Media { get; set; }
    }

}