using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Meta.DataAccess
{

    public class LabelView : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI label;
        private MemoryDataClient dataClient;

        public void Init(MemoryDataClient dataClient)
        {
            this.dataClient = dataClient;
        }

        private void Update()
        {
            label.text = dataClient.GetSingle<Counter>().value.ToString("00");
        }

    }

}