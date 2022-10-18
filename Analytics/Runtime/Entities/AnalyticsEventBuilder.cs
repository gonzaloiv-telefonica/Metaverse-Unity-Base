using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Analytics
{

    public class AnalyticsEventBuilder
    {

        private AnalyticsEvent analyticsEvent;

        public AnalyticsEventBuilder()
        {
            this.analyticsEvent = new AnalyticsEvent();
        }

        public AnalyticsEventBuilder SetKey(string key)
        {
            analyticsEvent.key = key;
            return this;
        }

        public AnalyticsEventBuilder AddLabel(string key, string value)
        {
            analyticsEvent.labels.Add(key, value);
            return this;
        }

        public AnalyticsEvent Build()
        {
            return analyticsEvent;
        }

    }

}