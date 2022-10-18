using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Meta.Global;

namespace Meta.VR
{

    public class EditorModeSetup : MonoBehaviour
    {

        [SerializeField] private GameObject ovrCameraRig;
        [SerializeField] private StandaloneInputModule standaloneInputModule;
        [SerializeField] private bool addEditorController = false;
        [SerializeField] private GameObject leftInteractor;

        private IEnumerator Start()
        {
            bool isEditor = !OVRManager.isHmdPresent;
            standaloneInputModule.enabled = isEditor;
            leftInteractor.SetActive(!isEditor);
            yield return new WaitForEndOfFrame();
            if (isEditor && addEditorController)
                ovrCameraRig.AddComponent<SimpleCameraController>();
        }

    }

}