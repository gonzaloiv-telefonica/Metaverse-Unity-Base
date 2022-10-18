using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Global
{

    public class DeleteGOsOnSceneChange : MonoBehaviour
    {

        [SerializeField] private List<GameObject> gos;

        private void OnDestroy()
        {
            foreach (GameObject go in gos)
                DestroyImmediate(go, true);
        }

    }

}