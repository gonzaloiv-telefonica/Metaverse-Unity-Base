using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityApplicationInsights;

namespace Meta.Analytics
{

    public class AzureAnalyticsProvider : TelemetryClient, IAnalyticsProvider
    {

        // ? Explicit implementation of the interface for information purposes
        public override void SetUserId(string userId = "")
        {
            base.SetUserId(userId);
        }

        public void Send(AnalyticsEvent analyticsEvent)
        {
            TrackEvent(analyticsEvent.ToEventTelemetry());
        }

    }

}