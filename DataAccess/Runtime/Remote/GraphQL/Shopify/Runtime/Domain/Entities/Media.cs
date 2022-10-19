using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.DataAccess;

namespace Meta.ShopifyStore
{

    [System.Serializable]
    public class Media
    {
        public List<Image> images;
        public List<Glb> glbs;

        public bool HasImages => images != null && images.Count > 0;
        public bool HasGLBs => glbs != null && glbs.Count > 0;

        public Media()
        {
            this.images = new List<Image>();
            this.glbs = new List<Glb>();
        }

    }

}