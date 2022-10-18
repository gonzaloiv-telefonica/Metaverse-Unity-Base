using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.FlowControl
{

    public class StateMachineSample : MonoBehaviour
    {

        private StateMachine stateMachine;

        [SerializeField] private GameObject cube;
        [SerializeField] private Button toSphereButton;

        [SerializeField] private GameObject sphere;
        [SerializeField] private Button toCubeButton;

        private void Start()
        {
            cube.SetActive(false);
            sphere.SetActive(false);
            toSphereButton.gameObject.SetActive(false);
            toSphereButton.onClick.AddListener(() => Router.SetCurrentState("SphereState"));
            toCubeButton.gameObject.SetActive(false);
            toCubeButton.onClick.AddListener(() => Router.SetCurrentState("CubeState"));
            Setup();
        }

        private void Setup()
        {
            this.stateMachine = new StateMachine();
            stateMachine.Register(
                new StateBuilder<State>()
                    .Route("CubeState")
                    .Enter(() =>
                    {
                        cube.SetActive(true);
                        toSphereButton.gameObject.SetActive(true);
                    })
                    .Exit(() =>
                    {
                        cube.SetActive(false);
                        toSphereButton.gameObject.SetActive(false);
                    })
                    .Build()
            );
            stateMachine.Register(
                new StateBuilder<State>()
                    .Route("SphereState")
                    .Enter(() =>
                    {
                        sphere.SetActive(true);
                        toCubeButton.gameObject.SetActive(true);
                    })
                    .Exit(() =>
                    {
                        sphere.SetActive(false);
                        toCubeButton.gameObject.SetActive(false);
                    })
                    .Build()
            );
            Router.Init(stateMachine);
            Router.SetCurrentState("CubeState");
        }

    }

}