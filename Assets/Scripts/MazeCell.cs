using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public int x;
    public int y;
   [SerializeField]
    private GameObject leftWall;

    [SerializeField]
    private GameObject rightWall;

    [SerializeField]
    private GameObject frontWall;

    [SerializeField]
    private GameObject backWall;

    [SerializeField]
    private GameObject unvisitedObject;
    [SerializeField]
    private GameObject mazeFloor;
    public bool visitedByPlayer = false;
    
    public bool isVistited{get;private set; }
    public void Visit()
    {
        isVistited = true;
        //unvisitedObject.SetActive(false);
        if(unvisitedObject != null){
            Destroy(unvisitedObject);
        }
    }
    public void ClearLeftWall()
    {
        //leftWall.SetActive(false);
        Destroy(leftWall);
    }
    public void ClearRightWall()
    {
        //rightWall.SetActive(false);
        Destroy(rightWall);
    }
    public void ClearFrontWall()
    {
        //frontWall.SetActive(false);
        Destroy(frontWall);
    }
    public void ClearBackWall()
    {
        //backWall.SetActive(false);
        Destroy(backWall);
    }
    public void ClearMazeFloor()
    {
        Destroy(mazeFloor);
    }
    public bool HasLeftWall()
    {
        return leftWall != null;
    }
    public bool HasRightWall()
    {
        return rightWall != null;
    }
    public bool HasBackWall()
    {
        return backWall != null;
    }
    public bool HasFrontWall()
    {
        return frontWall != null;
    }
    public void MarkAsVisited()
    {
        visitedByPlayer = true;
    }
    public void VisitAgain(){
        isVistited = false;
    }

    public override bool Equals(object obj)
    {
        if (obj is MazeCell other)
        {
            return x == other.x && y == other.y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return x * 73856093 ^ y * 19349663;
    }
}
