using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class RoadSegment : MonoBehaviour
{
    public List<Vector3> pointPositions;
    public SplinePoint[] splinePoints;

    private SplineComputer spline;

    private void Start()
    {
        spline = GetComponent<SplineComputer>();
        splinePoints = spline.GetPoints();

        foreach(SplinePoint point in splinePoints)
        {
            pointPositions.Add(point.position);
        }
    }
}
