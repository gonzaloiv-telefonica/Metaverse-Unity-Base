using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Meta.FlowControl
{

    public class SphereState : State
    {

        private const string SceneName = "SphereScene";
        private Button toCubeButton;
        private Scene scene;

        public SphereState(Button toCubeButton) : base()
        {
            this.toCubeButton = toCubeButton;
            this.toCubeButton.gameObject.SetActive(false);
            this.toCubeButton.onClick.AddListener(() => Router.SetCurrentState<CubeState>());
        }

        public override void Enter()
        {
            SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            toCubeButton.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            SceneManager.UnloadSceneAsync(SceneName);
            toCubeButton.gameObject.SetActive(false);
        }

    }

}