using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Meta.Global;
using Meta.DataAccess;

namespace Meta.ShopifyStore
{

    public class ShopifyMedia : IApiEntity<Meta.ShopifyStore.Media>
    {

        [JsonProperty("edges")]
        public MediaEdge[] Edges { get; set; }
        public bool HasValues => Edges != null && Edges.Length > 0;

        public ShopifyMedia()
        {
            Edges = new MediaEdge[0];
        }

        public partial class MediaEdge
        {
            [JsonProperty("node")]
            public FluffyNode Node { get; set; }
        }

        public Meta.ShopifyStore.Media ToEntity()
        {
            if (!HasValues)
                return new Media();
            Meta.ShopifyStore.Media media = new Meta.ShopifyStore.Media();
            foreach (MediaEdge edge in Edges)
            {
                switch (edge.Node.MediaContentType)
                {
                    case "IMAGE":
                        media.images.Add(ParseImage(edge.Node));
                        break;
                    case "MODEL_3D":
                        media.glbs.Add(ParseGlb(edge.Node));
                        break;
                }
            }
            return media;
        }

        private Meta.ShopifyStore.Image ParseImage(FluffyNode node)
        {
            Meta.ShopifyStore.Image image = new Meta.ShopifyStore.Image();
            image.url = node.Image.OriginalSrc.ToString();
            image.fileName = new Uri(image.url).ToFileName();
            return image;
        }

        private Meta.ShopifyStore.Glb ParseGlb(FluffyNode node)
        {
            Meta.ShopifyStore.Glb glb = new Meta.ShopifyStore.Glb();
            glb.url = node.Sources[0].Url.ToString();
            glb.fileName = new Uri(glb.url).ToFileName();
            return glb;
        }

        public partial class FluffyNode
        {
            [JsonProperty("mediaContentType")]
            public string MediaContentType { get; set; }

            [JsonProperty("alt")]
            public string Alt { get; set; }

            [JsonProperty("sources", NullValueHandling = NullValueHandling.Ignore)]
            public Source[] Sources { get; set; }

            [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
            public Image Image { get; set; }
        }

        public partial class Image
        {
            [JsonProperty("originalSrc")]
            public Uri OriginalSrc { get; set; }
        }

        public partial class Source
        {
            [JsonProperty("url")]
            public Uri Url { get; set; }

            [JsonProperty("mimeType")]
            public string MimeType { get; set; }

            [JsonProperty("format")]
            public string Format { get; set; }

            [JsonProperty("filesize")]
            public long Filesize { get; set; }
        }

    }


}