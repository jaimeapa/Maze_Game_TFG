using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
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
    public TextMeshProUGUI message;
    public PathFinder pathFinder;
    private bool stuck = false;
    public MazeCell cell = null;
    

    void Start()
    {
        mazeGenerator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        player = GameObject.Find("Player");
        raycastReticula = GameObject.Find("Main Camera").GetComponent<RaycastReticula>();
        playerMovement = GameObject.Find("Manager").GetComponent<PlayerMovement>();
        message = GameObject.Find("Debug").GetComponent<TextMeshProUGUI>();
        pathFinder = GameObject.Find("PathFinder").GetComponent<PathFinder>();
        enemyRb = GetComponent<Rigidbody>();
        //speedSlider = GameObject.FindWithTag("Speed").GetComponent<SpeedSlider>();
        position = 0;
        //speed = 1f;
        speed = 0;
        //List<MazeCell> visitedCells = new List<MazeCell>();
    }


   
    void FixedUpdate()
    {
        
        if (stuck && cell != null)
        {
            visitedCells = pathFinder.FindPath(cell);
        }else
        {
            visitedCells = raycastReticula.GetVisitedCells();
        }

        if(speed == 0)
        {
            speed = playerMovement.GetEnemySpeed();
        }
        
        //Debug.Log("Visited cells count: " + visitedCells.Count);
        startingPosition = playerMovement.GetStartingPos();
        if (visitedCells.Count == 0) {
            //Debug.LogError("Visited cells list is empty!");
            return;
        }
        if(position < visitedCells.Count){
            cell = visitedCells[position];
            
            
            if (position > 0 && isBlockedByWall(visitedCells[position - 1], visitedCells[position]))
            {
                visitedCells.RemoveAt(position);
            }
            
            currentTarget = new Vector3(cell.x + startingPosition.x + 0.2f, startingPosition.y - cell.y, startingPosition.z);
            Vector3 newPosition = Vector3.MoveTowards(enemyRb.position, currentTarget, speed * Time.fixedDeltaTime);
            enemyRb.MovePosition(newPosition);
            
            

            if (position > 0)
            {
                Quaternion targetRotation = Quaternion.Euler(GetNextRotation(visitedCells[position-1], visitedCells[position]), 90, - 90);
                enemyRb.MoveRotation(targetRotation);
            }
            //message.text = "Position: " + (int)(transform.position.x - startingPosition.x) + ","+ (int)(startingPosition.y - transform.position.y) + "\nCount: " + visitedCells.Count + "\nNext Cell: " + cell.x + "," + cell.y;
            //message.text += "\nSpeed: " + speed;
            //if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPosition = new Vector2(currentTarget.x, currentTarget.y);

            if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
            {
                //message.text += "\nPosition++ ";
                position++;
                //Debug.Log("Moving"); // Pasar al siguiente objetivo
            }
            /*if (enemyRb.velocity.magnitude < 0.01f)
            {
                stuck = true;
            }*/
            //message.text += "\nLoop ended";
            
        }
        
    }

    

    private bool isBlockedByWall(MazeCell previousCell, MazeCell nextCell)
    {
        if(previousCell.x < nextCell.x)
        {
            if (previousCell.HasRightWall())
            {
                return true;
            }
        }
        if (previousCell.x > nextCell.x)
        {
            if (previousCell.HasLeftWall())
            {
                return true;
            }
        }
        if (previousCell.y < nextCell.y)
        {
            if (previousCell.HasBackWall())
            {
                return true;
            }
        }
        if (previousCell.y > nextCell.y)
        {
            if (previousCell.HasFrontWall())
            {
                return true;
            }
        }
        return false;
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

