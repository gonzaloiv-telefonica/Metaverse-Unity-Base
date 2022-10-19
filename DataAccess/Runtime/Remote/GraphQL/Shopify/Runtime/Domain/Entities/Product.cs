using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.DataAccess;

namespace Meta.ShopifyStore
{

    [System.Serializable]
    public class Product : BaseEntity, IEntity
    {
        public string name;
        public Media media;
        public bool HasImages => media.HasImages;
        public bool HasGLBs => media.HasGLBs;
    }

}