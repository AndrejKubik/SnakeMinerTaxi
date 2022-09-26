using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector3 nextWagonSpawnPosition;

    [SerializeField] private List<GameObject> wagons;

    private List<Vector3> headPositionHistory;
    private List<Quaternion> headRotationHistory;

    private void Start()
    {
        headPositionHistory = new List<Vector3>(); //make an empty list for the previous head positions
        headRotationHistory = new List<Quaternion>(); //make an empty list for the previous head rotations

        nextWagonSpawnPosition = transform.position; //set the spawn for the first wagon to the train head position
    }

    private void Update()
    {
        if(TrainControl.IsMoving)
        {
            headPositionHistory.Insert(0, transform.position);
            headRotationHistory.Insert(0, transform.rotation);

            for (int i = 0; i < wagons.Count; i++)
            {
                Vector3 targetPosition = headPositionHistory[Mathf.Min(i * GameManager.SnakeGap, headPositionHistory.Count - 1)];
                wagons[i].transform.position = targetPosition;
                //wagons[i].transform.position = Vector3.MoveTowards(wagons[i].transform.position, targetPosition, TrainControl.TrainSpeed * 2f * Time.deltaTime);

                Quaternion targetRotation = headRotationHistory[Mathf.Min(i * GameManager.SnakeGap, headPositionHistory.Count - 1)];
                wagons[i].transform.rotation = targetRotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickup"))
        {
            AddWagon(); //when the player touches a pickup spawn a new wagon
            Destroy(other.gameObject); //destroy the pickup object
        }
    }

    public void AddWagon()
    {
        GameObject freshWagon = Instantiate(GameManager.WagonPrefab, nextWagonSpawnPosition, transform.rotation); //spawn a wagon prefab on the designated free position
        wagons.Add(freshWagon); //add the new wagon to the list of all wagons(snake body)
        nextWagonSpawnPosition = freshWagon.transform.position; //move the wagon spawn position to the new wagon's position
    }
}
