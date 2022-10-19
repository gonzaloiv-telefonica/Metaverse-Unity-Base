using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using System.Linq;
using Meta.Global;

namespace Meta.Timeline
{

    [System.Serializable]
    public class SplineAsset : PlayableAsset, ISplineAsset
    {

        public ExposedReference<Transform> spline;
        [Tooltip("Invierte el sentido del path")]
        public bool inverse = false;
        [Tooltip("Si el path está cerrado, esto sirve para orientar correctamente del ultimo al primer punto del path")]
        public bool loop;
        [Tooltip("Si es true, la velocidad es constante, si es false, es más lento en las curvas")]
        public bool costantSpeed;
        [Tooltip("Si es true, el objeto orientará su eje Z para que apunte en la dirección del movimiento")]
        public bool orientTowardsMovement;
        [HideInInspector] public double ClipStart { get; set; }
        [HideInInspector] public double RealDuration { get; set; }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            ScriptPlayable<SplineBehaviour> scriptPlayable = ScriptPlayable<SplineBehaviour>.Create(graph);
            SplineBehaviour behaviour = scriptPlayable.GetBehaviour();
            behaviour.pathPoints = spline.Resolve(graph.GetResolver()).GetComponent<CatmullRomSpline>().GetPositions().Flatten().ToArray();
            behaviour.routeTransform = spline.Resolve(graph.GetResolver());
            behaviour.inverse = inverse;
            behaviour.loop = loop;
            behaviour.constantSpeed = costantSpeed;
            behaviour.clipStart = ClipStart;
            behaviour.realDuration = RealDuration;
            behaviour.orientTowardsMovement = orientTowardsMovement;
            return scriptPlayable;
        }

    }

}