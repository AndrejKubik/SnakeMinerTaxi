using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Junction : MonoBehaviour
{
    public SplineComputer leftTrail;
    public SplineComputer rightTrail;
    public SplineComputer upTrail;
    public SplineComputer downTrail;

    public Spline.Direction leftDirection;
    public Spline.Direction rightDirection;
    public Spline.Direction upDirection;
    public Spline.Direction downDirection;

    private void Update()
    {
        if (GameManager.train.spline == leftTrail) leftDirection = Spline.Direction.Backward;
        else if (GameManager.train.spline != leftTrail) leftDirection = Spline.Direction.Forward;

        if (GameManager.train.spline == rightTrail) rightDirection = Spline.Direction.Backward;
        else if (GameManager.train.spline != rightTrail) rightDirection = Spline.Direction.Forward;

        if (GameManager.train.spline == upTrail) upDirection = Spline.Direction.Backward;
        else if (GameManager.train.spline != upTrail) upDirection = Spline.Direction.Forward;

        if (GameManager.train.spline == downTrail) downDirection = Spline.Direction.Backward;
        else if (GameManager.train.spline != downTrail) downDirection = Spline.Direction.Forward;
    }
}
