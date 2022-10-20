using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Meta.Timeline
{

    [TrackClipType(typeof(SplineAsset))]
    [TrackBindingType(typeof(Transform))]
    public class SplineTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (TimelineClip clip in m_Clips)
            {
                ISplineAsset splineAsset = clip.asset as ISplineAsset;
                splineAsset.RealDuration = clip.duration;
            }
            return base.CreateTrackMixer(graph, go, inputCount);
        }
    }

}