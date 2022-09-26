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
        if(TrainControl.IsMoving) //while train head is moving along a spline
        {
            //store the train head's position and rotation every frame for the target positions and rotations to move towards
            headPositionHistory.Insert(0, transform.position);
            headRotationHistory.Insert(0, transform.rotation);

            for (int i = 0; i < wagons.Count; i++) //for every wagon
            {
                //calculate the current wagon's target position and rotation by taking the trail gap into account

                Vector3 targetPosition = headPositionHistory[Mathf.Min(i * GameManager.SnakeGap, headPositionHistory.Count - 1)]; 
                wagons[i].transform.position = targetPosition;

                Quaternion targetRotation = headRotationHistory[Mathf.Min(i * GameManager.SnakeGap, headPositionHistory.Count - 1)];
                wagons[i].transform.rotation = targetRotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickup")) //when the player touches a pickup
        {
            AddWagon(); //spawn a new wagon
            GameManager.numberOfPickups--; //lower the count of total pickups on the level
            Destroy(other.gameObject); //destroy the pickup object
            if (GameManager.numberOfPickups <= 0) LevelWin(); //if the current pickup was the last one, player beats the level
        }
        else if(other.CompareTag("Wagon")) //when head collides with the trail
        {
            GameOver(); //lose the level
        }
    }

    public void AddWagon()
    {
        GameObject freshWagon = Instantiate(GameManager.WagonPrefab, nextWagonSpawnPosition, transform.rotation); //spawn a wagon prefab on the designated free position
        wagons.Add(freshWagon); //add the new wagon to the list of all wagons(snake body)
        nextWagonSpawnPosition = freshWagon.transform.position; //move the wagon spawn position to the new wagon's position
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        TrainControl.IsMoving = false;
        Time.timeScale = 0f;
    }

    public void LevelWin()
    {
        Debug.Log("Level Won");
        TrainControl.IsMoving = false;
        Time.timeScale = 0f;
    }
}
