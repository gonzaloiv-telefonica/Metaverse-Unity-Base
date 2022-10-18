using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Meta.FlowControl
{

    public class SceneState : State
    {

        private string[] sceneNames;

        public void SetSceneNames(params string[] sceneNames)
        {
            this.sceneNames = sceneNames;
        }

        public override void Enter()
        {
            base.Enter();
            foreach (string sceneName in sceneNames)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }

        public override void Exit()
        {
            base.Exit();
            foreach (string sceneName in sceneNames)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }

    }

}