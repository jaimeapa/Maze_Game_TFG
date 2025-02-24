using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class Enemy : MonoBehaviour
{
     
    public MazeGenerator mazeGenerator;
    public GameObject player;
    public List<MazeCell> visitedCells = new List<MazeCell>();
    public RaycastReticula raycastReticula; 
    private Vector3 currentTarget;
    public float speed;
    private int position;
    public Vector3 startingPosition = new Vector3(26.5f, 8.16f,20.64f);
    private Rigidbody enemyRb;
    private PlayerMovement playerMovement;
    private SpeedSlider speedSlider;

    void Start()
    {
        mazeGenerator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        player = GameObject.Find("Player");
        raycastReticula = GameObject.Find("Main Camera").GetComponent<RaycastReticula>();
        playerMovement = GameObject.Find("Manager").GetComponent<PlayerMovement>();
        enemyRb = GetComponent<Rigidbody>();
        //speedSlider = GameObject.FindWithTag("Speed").GetComponent<SpeedSlider>();
        position = 0;
        //speed = 1f;
        speed = 0;
        
    }


   
    void FixedUpdate()
    {
        List<MazeCell> visitedCells = raycastReticula.GetVisitedCells();
        if(speed == 0)
        {
            speed = playerMovement.GetEnemySpeed();
        }
        
        //Debug.Log("Visited cells count: " + visitedCells.Count);
        startingPosition = playerMovement.GetStartingPos();
        if (visitedCells.Count == 0) {
            Debug.LogError("Visited cells list is empty!");
            return;
        }
        if(position < visitedCells.Count){
            MazeCell cell = visitedCells[position];
            //Debug.Log(cell.x + ", " + cell.y);

            currentTarget = new Vector3(cell.x + startingPosition.x, startingPosition.y - cell.y, startingPosition.z);
            //transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
            Vector3 newPosition = Vector3.MoveTowards(enemyRb.position, currentTarget, speed * Time.fixedDeltaTime);
            enemyRb.MovePosition(newPosition);
            if(position > 0)
            {
                Quaternion targetRotation = Quaternion.Euler(GetNextRotation(visitedCells[position-1], visitedCells[position]), 90, - 90);
                enemyRb.MoveRotation(targetRotation);
            }
            

            if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
            {
                position++;
                Debug.Log("Moving"); // Pasar al siguiente objetivo
            }
        }
        
    }
    public int GetNextRotation(MazeCell previousCell, MazeCell nextCell)
    {
        int rotation = 0;
        if (previousCell.x < nextCell.x)
        {
            rotation = 0;
        }
        if(previousCell.y < nextCell.y)
        {
            rotation = 90;
        }
        if (previousCell.x > nextCell.x)
        {
            rotation = 180;
        }
        if (previousCell.y > nextCell.y)
        {
            rotation = 270;
        }

        return rotation;
    }

}

