using UnityEngine;
using UnityEngine.Playables;
using Meta.Global;

namespace Meta.Timeline
{

    [System.Serializable]
    public class SplineAsset : PlayableAsset, ISplineAsset
    {

        public ExposedReference<Transform> spline;
        public bool loop;
        public bool orientTowardsMovement;
        [HideInInspector] public double RealDuration { get; set; }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            ScriptPlayable<SplineBehaviour> scriptPlayable = ScriptPlayable<SplineBehaviour>.Create(graph);
            SplineBehaviour behaviour = scriptPlayable.GetBehaviour();
            behaviour.pathPoints = spline.Resolve(graph.GetResolver()).GetComponent<CatmullRomSpline>().GetPositions().Flatten().ToArray();
            behaviour.routeTransform = spline.Resolve(graph.GetResolver());
            behaviour.loop = loop;
            behaviour.realDuration = RealDuration;
            behaviour.orientTowardsMovement = orientTowardsMovement;
            return scriptPlayable;
        }

    }

}