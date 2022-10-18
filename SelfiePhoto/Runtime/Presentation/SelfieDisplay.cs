using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Meta.SelfiePhotos
{
    public class SelfieDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject display;

        private void Awake()
        {
            display.gameObject.transform.localScale = new Vector3(display.gameObject.transform.localScale.x * -1, display.gameObject.transform.localScale.y, display.gameObject.transform.localScale.z);
        }
    }
}