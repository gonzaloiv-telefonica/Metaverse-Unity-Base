using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;

namespace Meta.DataAccess
{

    [CreateAssetMenu(fileName = "Settings", menuName = "Meta/DataAccess/Settings")]
    public class Settings : ScriptableObject
    {
        public string baseUrl;
        public string token;
    }

}