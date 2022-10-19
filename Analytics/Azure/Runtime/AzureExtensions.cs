using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityApplicationInsights;

namespace Meta.Analytics
{

    public static class AzureExtensions
    {

        public static EventTelemetry ToEventTelemetry(this AnalyticsEvent analyticsEvent)
        {
            EventTelemetry eventTelemetry = new EventTelemetry();
            eventTelemetry.Name = analyticsEvent.key;
            eventTelemetry.Properties = analyticsEvent.labels;
            return eventTelemetry;
        }

    }

}