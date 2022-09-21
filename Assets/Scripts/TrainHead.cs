using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class TrainHead : MonoBehaviour
{
    private void Start()
    {
        GameManager.trailSpeed = GetComponent<SplineFollower>().followSpeed;
    }
}
