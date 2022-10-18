using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Meta.DataAccess
{

    public static class PostmanParser
    {

        public static List<T> ParseAsList<T, A>(string response, string uri) where T : new() where A : IApiEntity<T>, new()
        {
            JObject jObject = JObject.Parse(response);
            JArray objs = (JArray)jObject["data"][uri.ToString()];
            List<T> entities = new List<T>();
            foreach (JObject obj in objs)
            {
                entities.Add(obj.ToObject<A>().ToEntity());
            }
            return entities;
        }

    }

}
