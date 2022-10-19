using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.DataAccess;

namespace Meta.ShopifyStore
{

    public static class ShopifyParametersParser
    {

        private const int DefaultLimit = 10;

        public static Dictionary<string, object> Default => Parse(null);

        public static Dictionary<string, object> Parse(Parameters parameters = null)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("limit", parameters != null && parameters.HasLimit ? parameters.limit : DefaultLimit);
            return result;
        }

    }

}