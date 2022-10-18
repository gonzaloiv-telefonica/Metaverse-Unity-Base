using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Meta.Timeline
{

    public partial class SplineBehaviour : PlayableBehaviour
    {

        public bool inverse;
        public bool loop;
        public bool constantSpeed;
        public double clipStart, realDuration;
        public Vector3[] pathPoints;
        public Transform routeTransform;
        public bool orientTowardsMovement;

        private Transform transform;
        private Transform[] positions;
        private int currentIndex;
        private PathSegmentInfo[] pathSegmentsInfo;
        private PathSegmentInfo currentSegment = null;
        private Vector3 nextSegmentOrientation;

        public override void OnGraphStart(Playable playable)
        {
            pathSegmentsInfo = SWSUtil.CalculatePathInfo(pathPoints, realDuration, inverse);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            float currentTime = (float)playable.GetTime();
            float globalPercentage = (float)(playable.GetTime() / playable.GetDuration());
            if (globalPercentage > 1)
                return;
            float localPercentage;
            Vector3 forward;

            if (transform == null)
            {
                transform = (Transform)playerData;
            }
            else
            {
                if (constantSpeed)
                {
                    //Traslación
                    currentSegment = SWSUtil.CalculateCurrentSegment(pathSegmentsInfo, playable.GetTime(), globalPercentage);
                    localPercentage = (float)((currentTime - currentSegment.originTime) / (currentSegment.endTime - currentSegment.originTime));
                    transform.localPosition = Vector3.Lerp(currentSegment.originPosition, currentSegment.endPosition, localPercentage);
                    if (orientTowardsMovement)
                    {
                        //Rotación
                        nextSegmentOrientation = GetNextSegmentOrientation(currentSegment);
                        forward = (currentSegment.endPosition - currentSegment.originPosition);
                        //transform.LookAt(Vector3.Lerp(transform.localPosition + forward,currentSegment.endPosition + nextSegmentOrientation, localPercentage), Vector3.up);
                        Vector3 pointToLook = Vector3.Lerp(transform.localPosition + forward, currentSegment.endPosition + nextSegmentOrientation, localPercentage);
                        pointToLook = routeTransform.TransformPoint(pointToLook);
                        transform.LookAt(pointToLook, Vector3.up);
                    }
                }
                else
                {
                    //Traslación
                    int currentIndex = Mathf.FloorToInt(pathPoints.Length * globalPercentage);
                    if (currentIndex <= 0 || currentIndex >= pathPoints.Length - 1)
                        return;
                    Vector3 destination = loop && currentIndex == pathPoints.Length - 2 ? pathPoints[0] : pathPoints[currentIndex + 1];
                    Vector3 origin = pathPoints[currentIndex];
                    MoveSlowerInCurves(currentIndex, playable.GetDuration(), playable.GetTime(), destination, origin);
                    if (orientTowardsMovement)
                    {
                        //Rotación
                        Vector3 nextDestination;
                        if (loop)
                        {
                            if (currentIndex < pathPoints.Length - 2)
                                nextDestination = pathPoints[currentIndex + 2];
                            else
                                nextDestination = pathPoints[1];
                        }
                        else if (currentIndex < pathPoints.Length - 2)
                            nextDestination = pathPoints[currentIndex + 2];
                        else
                            nextDestination = pathPoints[currentIndex];

                        double timeSegment = playable.GetDuration() / pathPoints.Length;
                        double previousTime = currentIndex * timeSegment;
                        double segmentPercentage = (playable.GetTime() - previousTime) / timeSegment;
                        Vector3 newForware = (nextDestination - destination);
                        forward = (destination - origin);
                        Vector3 pointToLook = Vector3.Lerp(transform.localPosition + forward, destination + newForware, (float)segmentPercentage);
                        pointToLook = routeTransform.TransformPoint(pointToLook);
                        //Quaternion tempRotation = Quaternion.Euler(directionToLook);
                        transform.LookAt(pointToLook, Vector3.up);
                        //transform.localRotation += Quaternion.LookRotation(Vector3.Lerp(transform.localPosition + forward, destination + newForware, (float)segmentPercentage), Vector3.up);
                    }
                }
            }
        }

        /// <summary>
        /// Método para mover el objeto más lento en las curvas
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <param name="totalTime"></param>
        /// <param name="currentTime"></param>
        /// <param name="destination"></param>
        /// <param name="origin"></param>
        private void MoveSlowerInCurves(int currentIndex, double totalTime, double currentTime, Vector3 destination, Vector3 origin)
        {
            double timeSegment = totalTime / pathPoints.Length;
            double previousTime = currentIndex * timeSegment;
            double segmentPercentage = (currentTime - previousTime) / timeSegment;
            transform.localPosition = Vector3.Lerp(destination, origin, 1 - (float)segmentPercentage);
        }

        /// <summary>
        /// Devuelve la orientación hacia el siguiente segmento
        /// </summary>
        /// <param name="currentSegment"></param>
        /// <returns></returns>
        private Vector3 GetNextSegmentOrientation(PathSegmentInfo currentSegment)
        {
            Vector3 nextSegmentOrientation = Vector3.zero;

            if (currentSegment.endIndex == pathPoints.Length - 1)
            {
                if (loop)
                    nextSegmentOrientation = pathPoints[1] - pathPoints[currentSegment.endIndex];
                else
                    nextSegmentOrientation = pathPoints[currentSegment.endIndex] - pathPoints[currentSegment.endIndex - 1];
            }
            else
                nextSegmentOrientation = pathPoints[currentSegment.endIndex + 1] - pathPoints[currentSegment.endIndex];

            return nextSegmentOrientation;
        }

    }

}