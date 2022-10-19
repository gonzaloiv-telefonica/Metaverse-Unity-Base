using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Analytics
{

    [System.Serializable]
    public class AnalyticsEvent
    {

        public string key;
        public Dictionary<string, string> labels;

        public bool HasParameters => labels.Count > 0;

        public AnalyticsEvent()
        {
            labels = new Dictionary<string, string>();
        }

        public AnalyticsEvent(string key)
        {
            this.key = key;
        }

        public bool HasLabel(string key)
        {
            return labels.ContainsKey(key);
        }

    }

}