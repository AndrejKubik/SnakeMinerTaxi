using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;

public class GameManager : MonoBehaviour
{
    public static SplineFollower train;

    private float trailSpeed;

    private SplineComputer leftTrail;
    private SplineComputer rightTrail;
    private SplineComputer forwardTrail;

    private SplineTracer.NodeConnection reachedNode;

    [SerializeField] private bool oneChoice;
    [SerializeField] private bool atNode;

    private void Start()
    {
        train = GetComponent<SplineFollower>(); //store the spline follower 
        trailSpeed = train.followSpeed;

        train.onNode += NodeReached; //subscribe a method for the node touch event
    }

    private void NodeReached(List<SplineTracer.NodeConnection> passed)
    {
        if(!atNode)
        {
            reachedNode = passed[0]; //store the first touched node into a variable for simpliticy sake

            //get the connected trails from the touched node
            leftTrail = reachedNode.node.GetComponent<Junction>().leftTrail;
            rightTrail = reachedNode.node.GetComponent<Junction>().rightTrail;
            forwardTrail = reachedNode.node.GetComponent<Junction>().forwardTrail;

            if (leftTrail == null && rightTrail == null) oneChoice = true;

            if (oneChoice) GoForward(); //if there is only one trail choice, choose it automatically

            atNode = true;
        }
    }

    public void GoLeft()
    {
        train.followSpeed = 0f;
        train.enabled = false;
        train.SetPercent(0.0);
        train.spline = leftTrail;
        train.enabled = true;
        train.followSpeed = trailSpeed;

        StartCoroutine(UnblockJunctionSensor(0.2f));
    }

    public void GoRight()
    {
        train.followSpeed = 0f;
        train.enabled = false;
        train.SetPercent(0.0);
        train.spline = rightTrail;
        train.enabled = true;
        train.followSpeed = trailSpeed;

        StartCoroutine(UnblockJunctionSensor(0.2f));
    }

    public void GoForward()
    {
        train.followSpeed = 0f;
        train.enabled = false;
        train.SetPercent(0.0);
        train.spline = forwardTrail;
        train.enabled = true;
        train.followSpeed = trailSpeed;

        StartCoroutine(UnblockJunctionSensor(0.2f));

        oneChoice = false;
    }

    private void ClearChoices()
    {
        leftTrail = null;
        rightTrail = null;
        forwardTrail = null;
    }

    private IEnumerator UnblockJunctionSensor(float delay)
    {
        ClearChoices();
        yield return new WaitForSeconds(delay);
        atNode = false;
    }
}
