using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.FlowControl
{

    public class CustomClassStateSample : MonoBehaviour
    {

        [SerializeField] private GameObject cube;
        [SerializeField] private Button toSphereButton;
        [SerializeField] private Button toCubeButton;

        private void Start()
        {
            cube.SetActive(false);
            StateMachine stateMachine = new StateMachine();
            stateMachine.Register(new CubeState(cube, toSphereButton));
            stateMachine.Register(new SphereState(toCubeButton));
            Router.Init(stateMachine);
            Router.SetCurrentState<CubeState>();
        }

    }

}