using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Meta.Global
{

    public class fpsCounter : MonoBehaviour
    {

        public TMP_Text text;
        public int frames;

        private void Start()
        {
            StartCoroutine(CounterRoutine());
        }

        private void Update()
        {
            frames++;
        }

        private IEnumerator CounterRoutine()
        {
            yield return new WaitForSeconds(1);
            text.text = "FPS: " + frames.ToString();
            frames = 0;
            StartCoroutine(CounterRoutine());
        }

    }

}