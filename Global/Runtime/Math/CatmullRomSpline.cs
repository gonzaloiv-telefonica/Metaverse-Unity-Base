using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Meta.Global
{

    [ExecuteInEditMode]
    public class CatmullRomSpline : MonoBehaviour
    {
        public Transform[] points; // Less than 4 points mean a straight line
        public bool isLooping = true;
        [Range(0f, 0.5f)] public float resolution = 0.2f;

        public bool HasPoints { get { return points != null && points.Length > 0; } }

        private void OnDrawGizmos()
        {
            if (!HasPoints) return;
            List<List<Vector3>> positions = GetPositions();
            for (int segmentIndex = 0; segmentIndex < positions.Count; segmentIndex++)
            {
                for (int positionIndex = 0; positionIndex < positions[segmentIndex].Count - 1; positionIndex++)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(positions[segmentIndex][positionIndex], positions[segmentIndex][positionIndex + 1]);
                }
            }
            foreach (Transform point in points)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(point.position, 0.08f);
                Transform[] children = point.GetComponentsInChildren<Transform>();
                foreach (Transform child in children)
                {
                    Gizmos.color = Color.blue;
                    if (child != point)
                        Gizmos.DrawSphere(child.position, 0.04f);
                }
            }
        }

        public List<List<Vector3>> GetPositions()
        {
            Assert.AreNotEqual(points.Length, 0, "At least one point must be referenced in the editor");
            List<List<Vector3>> positions = new List<List<Vector3>>();
            for (int i = 0; i < points.Length; i++)
            {
                if (isLooping || i != points.Length - 1)
                {
                    positions.Add(GetSegmentPositions(i)); ;
                }
            }
            return positions;
        }

        private List<Vector3> GetSegmentPositions(int pos)
        {
            List<Vector3> positions = new List<Vector3>();
            Vector3 p0 = points[ClampPoints(pos - 1)].position;
            Vector3 p1 = points[pos].position;
            Vector3 p2 = points[ClampPoints(pos + 1)].position;
            Vector3 p3 = points[ClampPoints(pos + 2)].position;

            // How many times should we loop?
            int loops = Mathf.FloorToInt(1f / resolution);

            for (int i = 1; i <= loops; i++)
            {
                // Which t position are we at?
                float t = i * resolution;

                // Find the coordinate between the end points with a Catmull-Rom spline
                positions.Add(GetCatmullRomPosition(t, p0, p1, p2, p3));

            }

            return positions;
        }

        private int ClampPoints(int pos)
        {
            if (pos < 0)
            {
                pos = points.Length - 1;
            }

            if (pos > points.Length)
            {
                pos = 1;
            }
            else if (pos > points.Length - 1)
            {
                pos = 0;
            }
            return pos;
        }

        //Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
        //http://www.iquilezles.org/www/articles/minispline/minispline.htm
        private Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            //The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
            Vector3 a = 2f * p1;
            Vector3 b = p2 - p0;
            Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
            Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

            //The cubic polynomial: a + b * t + c * t^2 + d * t^3
            Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

            return pos;
        }

    }

}