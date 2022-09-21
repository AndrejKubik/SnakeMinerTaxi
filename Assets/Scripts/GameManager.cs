using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;

public class GameManager : MonoBehaviour
{
    public SplineFollower train;

    public static float trailSpeed;

    private SplineComputer leftTrail;
    private SplineComputer rightTrail;
    private SplineComputer upTrail;
    private SplineComputer downTrail;

    private Spline.Direction leftDirection;
    private Spline.Direction rightDirection;
    private Spline.Direction upDirection;
    private Spline.Direction downDirection;

    private SplineTracer.NodeConnection reachedNode;

    public bool nodeEntered;

    private void Start()
    {
        train.onNode += NodeReached; //subscribe a method for the node touch event
    }

    //JUNCTION APPROACH
    private void NodeReached(List<SplineTracer.NodeConnection> passed)
    {
        nodeEntered = !nodeEntered; //change the juntion-enter state

        if (nodeEntered) //if the junction has just been entered
        {
            reachedNode = passed[0]; //store the first touched node into a variable for simpliticy sake

            LoadJunctionData();

            BlockReverse();

            ShowJunctionButtons();
        }
    }
    private void LoadJunctionData()
    {
        //get the connected trails from the touched node
        leftTrail = reachedNode.node.GetComponent<Junction>().leftTrail;
        rightTrail = reachedNode.node.GetComponent<Junction>().rightTrail;
        upTrail = reachedNode.node.GetComponent<Junction>().upTrail;
        downTrail = reachedNode.node.GetComponent<Junction>().downTrail;

        //get the correct follow direction for each connected trail from the node
        leftDirection = reachedNode.node.GetComponent<Junction>().leftDirection;
        rightDirection = reachedNode.node.GetComponent<Junction>().rightDirection;
        upDirection = reachedNode.node.GetComponent<Junction>().upDirection;
        downDirection = reachedNode.node.GetComponent<Junction>().downDirection;
    }
    private void ShowJunctionButtons()
    {
        if (leftTrail != null) UIController.LeftButton.SetActive(true);
        if (rightTrail != null) UIController.RightButton.SetActive(true);
        if (upTrail != null) UIController.UpButton.SetActive(true);
        if (downTrail != null) UIController.DownButton.SetActive(true);
    }

    //BUTTON CONTROL
    public void GoLeft()
    {
        if (leftTrail != null)
        {
            ChangeTrail(leftTrail);

            SetCorrectDirection(leftDirection);

            ClearChoices();
        }
    }
    public void GoRight()
    {
        if(rightTrail != null)
        {
            ChangeTrail(rightTrail);

            SetCorrectDirection(rightDirection);

            ClearChoices();
        }
    }
    public void GoDown()
    {
        if(downTrail != null)
        {
            ChangeTrail(downTrail);

            SetCorrectDirection(downDirection);

            ClearChoices();
        }
    }
    public void GoUp()
    {
        if(upTrail != null)
        {
            ChangeTrail(upTrail);

            SetCorrectDirection(upDirection);
            
            ClearChoices();
        }
    }

    //TRAIL SWITCH
    private void ChangeTrail(SplineComputer targetTrail)
    {
        train.followSpeed = 0f;
        train.spline = targetTrail;
    }
    private void SetCorrectDirection(Spline.Direction targetDirection)
    {
        if (targetDirection == Spline.Direction.Forward)
        {
            train.SetPercent(0.0);
            train.followSpeed = trailSpeed;
        }
        else if (targetDirection == Spline.Direction.Backward)
        {
            train.SetPercent(1.0);
            train.followSpeed = -trailSpeed;
        }
    }
    private void BlockReverse()
    {
        if (upTrail == train.spline) upTrail = null;
        if (downTrail == train.spline) downTrail = null;
        if (leftTrail == train.spline) leftTrail = null;
        if (rightTrail == train.spline) rightTrail = null;
    }
    private void ClearChoices()
    {
        leftTrail = null;
        rightTrail = null;
        upTrail = null;
        downTrail = null;
    }
}
