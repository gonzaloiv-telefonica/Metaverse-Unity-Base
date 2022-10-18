using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Meta.FlowControl;

namespace Meta.Panels
{

    public class FlowMap : MonoBehaviour
    {

        [SerializeField] private OVRInput.Button buttonCode = OVRInput.Button.Four;
        [SerializeField] private GameObject content;
        [SerializeField] private List<StatePanel> panels;

        private FlowData data;

        private void Awake()
        {
            content.SetActive(false);
        }

        public void SetFlowData(FlowData data)
        {
            this.data = data;
        }

        private void Show()
        {
            panels.ForEach(panel => panel.Hide());
            for (int i = 0; i < data.states.Count; i++)
            {
                if (i >= panels.Count)
                    InstantiatePanel();
                string route = data.states[i].route;
                Action onClick = () =>
                {
                    Router.SetCurrentState(route);
                    content.SetActive(false);
                };
                panels[i].Show(data.states[i], onClick, Router.IsCurrentState(route));
            }
            content.SetActive(true);
        }

        public void Hide()
        {
            content.SetActive(false);
        }

        private void InstantiatePanel()
        {
            StatePanel panel = Instantiate(panels[0], panels[0].transform.parent)
                .GetComponent<StatePanel>();
            panels.Add(panel);
        }

        private void Update()
        {
            if (OVRInput.GetDown(buttonCode) || Input.GetKeyDown(KeyCode.Tab))
            {
                if (content.activeInHierarchy)
                {
                    Hide();
                }
                else
                {
                    Show();
                }
            }
        }

    }

}