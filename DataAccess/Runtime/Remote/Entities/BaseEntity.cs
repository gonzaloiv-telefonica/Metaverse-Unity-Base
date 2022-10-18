using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    /// <summary>
    /// Base class for classes that implement IEntity and have common id, createdAt and updatedAt fields
    /// </summary>
    [System.Serializable]
    public class BaseEntity : IEntity
    {
        public string id;
        public string createdAt;
        public string updatedAt;
        public string Id => id;
    }

}