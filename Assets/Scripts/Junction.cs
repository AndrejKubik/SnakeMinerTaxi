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
}
