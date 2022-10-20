using UnityEngine;
using UnityEngine.Playables;

namespace Meta.Timeline
{

    public partial class SplineBehaviour : PlayableBehaviour
    {

        public double realDuration;
        public Vector3[] pathPoints;
        public Transform routeTransform;
        public bool orientTowardsMovement;
        public bool loop;

        private Transform transform;
        private PathSegmentInfo[] pathSegmentsInfo;

        public override void OnGraphStart(Playable playable)
        {
            pathSegmentsInfo = SWSUtil.CalculatePathInfo(pathPoints, realDuration, loop);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (transform == null)
            {
                transform = (Transform)playerData;
                return;
            }
            float currentTime = (float)playable.GetTime();
            float globalPercentage = (float)(currentTime / playable.GetDuration());
            if (globalPercentage > 1)
                return;
            PathSegmentInfo currentSegment = SWSUtil.CalculateCurrentSegment(pathSegmentsInfo, playable.GetTime(), globalPercentage);
            float localPercentage = (float)((currentTime - currentSegment.originTime) / (currentSegment.endTime - currentSegment.originTime));
            ApplyMovement(currentSegment, localPercentage);
            ApplyRotation(currentSegment, localPercentage);
        }

        private void ApplyMovement(PathSegmentInfo currentSegment, float localPercentage)
        {
            transform.localPosition = Vector3.Lerp(currentSegment.originPosition, currentSegment.endPosition, localPercentage);
        }

        private void ApplyRotation(PathSegmentInfo currentSegment, float localPercentage)
        {
            if (!orientTowardsMovement)
                return;
            int nextIndex = currentSegment.endIndex + 1;
            if (nextIndex >= pathPoints.Length)
                nextIndex = 0;
            Vector3 nextSegmentOrientation = pathPoints[nextIndex] - pathPoints[currentSegment.endIndex];
            Vector3 forward = currentSegment.endPosition - currentSegment.originPosition;
            Vector3 pointToLook = Vector3.Lerp(transform.localPosition + forward, currentSegment.endPosition + nextSegmentOrientation, localPercentage);
            pointToLook = routeTransform.TransformPoint(pointToLook);
            transform.LookAt(pointToLook, Vector3.up);
        }

    }

}