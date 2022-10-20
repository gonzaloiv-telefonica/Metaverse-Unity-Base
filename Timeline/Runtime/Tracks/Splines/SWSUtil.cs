using UnityEngine;

namespace Meta.Timeline
{

    public static class SWSUtil
    {

        private static float addedLength = 0f;

        public static PathSegmentInfo[] CalculatePathInfo(Vector3[] pathPoints, double realDuration, bool loop = true)
        {
            addedLength = 0f;
            PathSegmentInfo[] pathSegmentsInfo = CalculateForLoop(pathPoints, loop);
            SetPathSegmentsTime(pathSegmentsInfo, realDuration);
            return pathSegmentsInfo;
        }

        private static PathSegmentInfo[] CalculateForLoop(Vector3[] pathPoints, bool loop)
        {
            int totalLength = loop ? pathPoints.Length : pathPoints.Length - 1;
            PathSegmentInfo[] pathSegmentsInfo = new PathSegmentInfo[totalLength];
            for (int i = 0; i < totalLength; i++)
            {
                pathSegmentsInfo[i] = new PathSegmentInfo();
                pathSegmentsInfo[i].originPosition = pathPoints[i];
                int endIndex = (i == (pathPoints.Length - 1)) && loop ? 0 : i + 1;
                pathSegmentsInfo[i].endIndex = endIndex;
                pathSegmentsInfo[i].endPosition = pathPoints[endIndex];
                pathSegmentsInfo[i].lenght = (pathPoints[endIndex] - pathPoints[i]).magnitude;
                pathSegmentsInfo[i].startLenght = addedLength;
                addedLength += pathSegmentsInfo[i].lenght;
            }
            return pathSegmentsInfo;
        }

        private static void SetPathSegmentsTime(PathSegmentInfo[] pathSegmentsInfo, double realDuration)
        {
            float addedPercentage = 0f;
            for (int i = 0; i < pathSegmentsInfo.Length; i++)
            {
                pathSegmentsInfo[i].percentage = pathSegmentsInfo[i].lenght / addedLength;
                addedPercentage += pathSegmentsInfo[i].percentage;
                if (i == 0)
                {
                    pathSegmentsInfo[i].originTime = 0f;
                }
                else
                {
                    pathSegmentsInfo[i].originTime = pathSegmentsInfo[i - 1].endTime;
                }
                pathSegmentsInfo[i].endTime = addedPercentage * realDuration;
            }
        }

        public static PathSegmentInfo CalculateCurrentSegment(PathSegmentInfo[] pathSegmentsInfo, double currentTime, float globalPercentage)
        {
            PathSegmentInfo currentSegment = pathSegmentsInfo[0];
            int segmentIndex = Mathf.FloorToInt(pathSegmentsInfo.Length * globalPercentage);
            while (segmentIndex >= 0 && segmentIndex < pathSegmentsInfo.Length)
            {
                currentSegment = pathSegmentsInfo[segmentIndex];
                if (IsBetween(currentTime, currentSegment.originTime, currentSegment.endTime))
                {
                    break;
                }
                else if (currentTime < currentSegment.originTime)
                {
                    segmentIndex--;
                }
                else if (currentTime >= currentSegment.endTime)
                {
                    segmentIndex++;
                }
            }
            return currentSegment;
        }

        private static bool IsBetween(double number, double minRange, double maxRange)
        {
            return (number >= minRange) && (number < maxRange);
        }

    }

}