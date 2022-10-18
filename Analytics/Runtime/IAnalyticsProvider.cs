using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;

namespace Meta.Analytics
{

    public interface IAnalyticsProvider
    {
        void SetUserId(string userId);
        void Send(AnalyticsEvent analyticsEvent);
    }

}