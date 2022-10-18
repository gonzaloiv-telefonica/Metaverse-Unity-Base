using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.FlowControl
{

    public class SceneStateBuilder<T> : StateBuilder<T> where T : SceneState, new()
    {

        public SceneStateBuilder<T> Scenes(params string[] sceneNames)
        {
            state.SetSceneNames(sceneNames);
            return this;
        }

    }

}