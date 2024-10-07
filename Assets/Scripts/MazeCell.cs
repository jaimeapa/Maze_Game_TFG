using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
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
    
    public bool isVistited{get;private set; }
    public void Visit()
    {
        isVistited = true;
        //unvisitedObject.SetActive(false);
        Destroy(unvisitedObject);
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
}
