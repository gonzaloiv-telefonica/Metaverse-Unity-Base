using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Meta.Configuration;

namespace Meta.Analytics
{

    public class AnalyticsEventsSample : MonoBehaviour
    {

        [SerializeField] private AzureAnalyticsProvider provider;
        [SerializeField] private Button event1Button;
        [SerializeField] private Button event2Button;
        private AnalyticsClient client;

        private void Start()
        {
            provider.SetUserId("MyUserId");
            event1Button.onClick.AddListener(SendEvent1);
            event2Button.onClick.AddListener(SendEvent2);
            client = new AnalyticsClient(provider);
            SendOnSessionStartEvent();
        }

        private void OnApOnApplicationQuit()
        {
            SendOnSessionEndEvent();
        }

        private void SendEvent1()
        {
            AnalyticsEvent analyticsEvent = new AnalyticsEventBuilder()
                .SetKey("OnExperienceLaunch")
                .AddLabel("ExperienceId", "1")
                .Build();
            client.Send(analyticsEvent);
        }

        private void SendEvent2()
        {
            AnalyticsEvent analyticsEvent = new AnalyticsEventBuilder()
                .SetKey("OnExperienceLaunch")
                .AddLabel("ExperienceId", "2")
                .Build();
            client.Send(analyticsEvent);
        }

        private void SendOnSessionStartEvent()
        {
            AnalyticsEvent analyticsEvent = new AnalyticsEventBuilder()
                .SetKey("OnSessionStart")
                .Build();
            client.Send(analyticsEvent);
        }

        private void SendOnSessionEndEvent()
        {
            AnalyticsEvent analyticsEvent = new AnalyticsEventBuilder()
                .SetKey("OnSessionEnd")
                .Build();
            client.Send(analyticsEvent);
        }

    }

}