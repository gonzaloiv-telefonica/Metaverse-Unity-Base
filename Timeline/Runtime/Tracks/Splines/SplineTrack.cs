using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Meta.Timeline
{

    [TrackClipType(typeof(SplineAsset))]
    [TrackBindingType(typeof(Transform))]
    public class SplineTrack : TrackAsset
    {
        /// <summary>
        /// Se accede aquí para saber la duración del clip antes de tener que entrar en el Process Frame
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="go"></param>
        /// <param name="inputCount"></param>
        /// <returns></returns>
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (TimelineClip clip in m_Clips)
            {
                ISplineAsset splineAsset = clip.asset as ISplineAsset;
                splineAsset.RealDuration = clip.duration;
                splineAsset.ClipStart = clip.start;
            }

            return base.CreateTrackMixer(graph, go, inputCount);
        }

    }

}