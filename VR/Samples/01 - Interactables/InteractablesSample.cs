using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Meta.VR
{

    public class InteractablesSample : MonoBehaviour
    {

        [SerializeField] private InteractableBehaviour sphere;
        [SerializeField] private InteractableBehaviour cube;

        [SerializeField] private TextMeshProUGUI label;

        private void Awake()
        {
            sphere.SetState(InteractableState.Enabled);
            cube.SetState(InteractableState.Disabled);
            sphere.AddListener(data =>
            {
                label.text = "Sphere interaction";
                sphere.SetState(InteractableState.Disabled);
                cube.SetState(InteractableState.Enabled);
            });
            cube.AddListener(data =>
            {
                label.text = "Cube interaction";
                sphere.SetState(InteractableState.Enabled);
                cube.SetState(InteractableState.Disabled);
            });
        }

    }

}