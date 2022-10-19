using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Timeline
{

    public static class SWSUtil
    {

        /// <summary>
        /// Aquí se calculan la información de los diferentes segmentos que componen el path.
        /// </summary>
        public static PathSegmentInfo[] CalculatePathInfo(Vector3[] pathPoints, double realDuration, bool inverse = false)
        {

            if (inverse)
                Array.Reverse(pathPoints);

            PathSegmentInfo[] pathSegmentsInfo = new PathSegmentInfo[pathPoints.Length - 1];
            float addedLength = 0f;
            float addedPercentage = 0f;

            //Guardamos las longitudes
            for (int i = 0; i < pathPoints.Length - 1; i++)
            {
                pathSegmentsInfo[i] = new PathSegmentInfo();
                pathSegmentsInfo[i].originIndex = i;
                pathSegmentsInfo[i].endIndex = i + 1;
                pathSegmentsInfo[i].originPosition = pathPoints[i];
                pathSegmentsInfo[i].endPosition = pathPoints[i + 1];
                pathSegmentsInfo[i].lenght = (pathPoints[i + 1] - pathPoints[i]).magnitude;
                pathSegmentsInfo[i].startLenght = addedLength;
                addedLength += pathSegmentsInfo[i].lenght;
            }

            //Calculamos los porcentajes y tiempos
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

            return pathSegmentsInfo;

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