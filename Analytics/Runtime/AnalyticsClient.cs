using System.Collections;
using System.Collections.Generic;
using RSG;
using UnityEngine;

namespace Meta.Analytics
{

    public class AnalyticsClient : IAnalyticsProvider
    {

        private IAnalyticsProvider provider;

        public AnalyticsClient(IAnalyticsProvider provider)
        {
            this.provider = provider;
        }

        public void SetUserId(string userId)
        {
            provider.SetUserId(userId);
        }

        public void Send(AnalyticsEvent analyticsEvent)
        {
            provider.Send(analyticsEvent);
        }

        public void SendOnSessionStartEvent()
        {
            AnalyticsEvent analyticsEvent = new AnalyticsEventBuilder()
                .SetKey("OnSessionStart")
                .Build();
            Send(analyticsEvent);
        }

        public void SendOnSessionEndEvent()
        {
            AnalyticsEvent analyticsEvent = new AnalyticsEventBuilder()
                .SetKey("OnSessionEnd")
                .Build();
            Send(analyticsEvent);
        }

    }

}