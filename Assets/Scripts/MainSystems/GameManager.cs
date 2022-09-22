using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public List<SplineComputer> paths;

    public List<Vector3> triggerWorldPositions;
    public List<double> triggerSplinePositions;

    [SerializeField] private Transform pickupsParent;
    [SerializeField] private GameObject pickupPrefab;

    private void Start()
    {
        LoadPickupSpawnData();

        SpawnPickupObjects();

        //GeneratePickupTriggers();
    }

    private void LoadPickupSpawnData()
    {
        foreach (SplineComputer path in paths) //for every spline
        {
            SplinePoint[] points = path.GetPoints(); //load all of the spline's control points

            for(int i = 1; i < points.Length - 1; i++)
            {
                triggerWorldPositions.Add(points[i].position); //store the point's world position for the pickup prefab spawn
                triggerSplinePositions.Add(path.GetPointPercent(i)); //store the point's distance percent along the current spline for the trigger generation 
            }
        }
    }

    private void SpawnPickupObjects()
    {
        for(int i = 0; i < triggerWorldPositions.Count; i++) //at every stored trigger position
        {
            GameObject pickup = Instantiate(pickupPrefab, triggerWorldPositions[i], transform.rotation, pickupsParent); //spawn a pickup prefab as a child object of the Pickups parent
            pickup.name = "Pickup" + (i + 1); //change the spawned object's name for simplicity sake
        }
    }

    //private void GeneratePickupTriggers()
    //{
    //    foreach (SplineComputer path in paths) //in every spline
    //    {
    //        for (int i = 1; i < path.pointCount - 1; i++) //for every control point of the current spline besides the first and last points(junction points)
    //        {
    //            SplineTrigger trigger = path.AddTrigger(0, triggerSplinePositions[i - 1], SplineTrigger.Type.Double, "Pickup" + i, Color.green); //add a new pickup trigger to the according position from the stored list of positions
    //            trigger.workOnce = true; //set the new trigger to single-use only
    //        }
    //    } 
    //}
}
