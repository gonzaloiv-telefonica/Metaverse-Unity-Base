using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.DataAccess
{

    public class ButtonsView : MonoBehaviour
    {

        [SerializeField] private Button substractionButton;
        [SerializeField] private Button additionButton;
        private MemoryDataClient dataClient;

        public void Init(MemoryDataClient dataClient)
        {
            this.dataClient = dataClient;
        }

        private void OnEnable()
        {
            substractionButton.onClick.AddListener(OnSubstractionButtonClick);
            additionButton.onClick.AddListener(OnAdditionButtonClick);
        }

        private void OnSubstractionButtonClick()
        {
            dataClient.GetSingle<Counter>().value--;
        }

        private void OnAdditionButtonClick()
        {
            dataClient.GetSingle<Counter>().value++;
        }

        private void OnDisable()
        {
            substractionButton.onClick.RemoveListener(OnSubstractionButtonClick);
            additionButton.onClick.RemoveListener(OnAdditionButtonClick);
        }


    }

}