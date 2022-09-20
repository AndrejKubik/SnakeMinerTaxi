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
    private SplineComputer upTrail;
    private SplineComputer downTrail;

    private Spline.Direction leftDirection;
    private Spline.Direction rightDirection;
    private Spline.Direction upDirection;
    private Spline.Direction downDirection;

    private SplineTracer.NodeConnection reachedNode;

    [SerializeField] private bool oneChoice;
    [SerializeField] private bool atNode;

    private void Start()
    {
        train = GetComponent<SplineFollower>(); //store the spline follower 
        trailSpeed = train.followSpeed;

        train.onNode += NodeReached; //subscribe a method for the node touch event

        train.spline.linearAverageDirection = true;
    }

    private void NodeReached(List<SplineTracer.NodeConnection> passed)
    {
        if(!atNode)
        {
            reachedNode = passed[0]; //store the first touched node into a variable for simpliticy sake

            //get the connected trails from the touched node
            leftTrail = reachedNode.node.GetComponent<Junction>().leftTrail;
            leftDirection = reachedNode.node.GetComponent<Junction>().leftDirection;

            rightTrail = reachedNode.node.GetComponent<Junction>().rightTrail;
            rightDirection = reachedNode.node.GetComponent<Junction>().rightDirection;

            upTrail = reachedNode.node.GetComponent<Junction>().upTrail;
            upDirection = reachedNode.node.GetComponent<Junction>().upDirection;

            downTrail = reachedNode.node.GetComponent<Junction>().downTrail;
            downDirection = reachedNode.node.GetComponent<Junction>().downDirection;

            atNode = true;
        }
    }

    public void GoLeft()
    {
        if (leftTrail != null)
        {
            train.direction = leftDirection;
            train.followSpeed = 0f;
            train.enabled = false;
            train.SetPercent(0.0);
            train.spline = leftTrail;
            train.enabled = true;
            train.followSpeed = trailSpeed;

            StartCoroutine(UnblockJunctionSensor(0.2f));
        }
    }

    public void GoRight()
    {
        if(rightTrail != null)
        {
            train.direction = rightDirection;
            train.followSpeed = 0f;
            train.enabled = false;
            train.SetPercent(0.0);
            train.spline = rightTrail;
            train.enabled = true;
            train.followSpeed = trailSpeed;

            StartCoroutine(UnblockJunctionSensor(0.2f));
        }
    }

    public void GoDown()
    {
        if(downTrail != null)
        {
            train.direction = downDirection;
            train.followSpeed = 0f;
            train.enabled = false;
            train.SetPercent(0.0);
            train.spline = downTrail;
            train.enabled = true;
            train.followSpeed = trailSpeed;

            StartCoroutine(UnblockJunctionSensor(0.2f));
        }
    }

    public void GoUp()
    {
        if(upTrail != null)
        {
            train.direction = upDirection;
            train.followSpeed = 0f;
            train.enabled = false;
            train.SetPercent(0.0);
            train.spline = upTrail;
            train.enabled = true;
            train.followSpeed = trailSpeed;

            StartCoroutine(UnblockJunctionSensor(0.2f));
        }
    }

    private void ClearChoices()
    {
        leftTrail = null;
        rightTrail = null;
        upTrail = null;
        downTrail = null;
    }

    private IEnumerator UnblockJunctionSensor(float delay)
    {
        ClearChoices();
        yield return new WaitForSeconds(delay);
        atNode = false;
    }
}
