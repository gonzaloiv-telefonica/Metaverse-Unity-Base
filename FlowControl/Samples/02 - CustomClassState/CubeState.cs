using UnityEngine;
using UnityEngine.UI;

namespace Meta.FlowControl
{
    
    public class CubeState : State
    {

        private GameObject cube;
        private Button toSphereButton;

        public CubeState(GameObject cube, Button toSphereButton) : base()
        {
            this.cube = cube;
            this.toSphereButton = toSphereButton;
            this.toSphereButton.gameObject.SetActive(false);
            this.toSphereButton.onClick.AddListener(() => Router.SetCurrentState<SphereState>());
        }

        public override void Enter()
        {
            cube.SetActive(true);
            toSphereButton.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            cube.SetActive(false);
            toSphereButton.gameObject.SetActive(false);
        }

    }

}