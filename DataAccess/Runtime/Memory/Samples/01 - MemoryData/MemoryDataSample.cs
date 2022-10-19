using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    public class MemoryDataSample : MonoBehaviour
    {

        [SerializeField] private LabelView labelView;
        [SerializeField] private ButtonsView buttonsView;

        private Counter counter;
        private MemoryDataClient dataClient;

        private void Awake()
        {
            counter = new Counter();
            dataClient = new MemoryDataClient();
            dataClient.PutSingle(counter);
            labelView.Init(dataClient);
            buttonsView.Init(dataClient);
        }


    }

}