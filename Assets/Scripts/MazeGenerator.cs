using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell mazeCellPrefab;

    [SerializeField]
    private int mazeWidth;

    [SerializeField]
    private int mazeDepth;

    [SerializeField]
    private GameObject finishLine;
    private GameObject finishLineToDestroy;
    private MazeCell[,] mazeGrid;
    private Vector3 startingPosition = new Vector3(26.5f, 8.16f,20.64f);
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void CreateMaze()
    {
        Debug.Log("Maze is Being created");
        mazeGrid = new MazeCell[mazeWidth, mazeDepth];

        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeDepth; j++)
            {
                mazeGrid[i,j] = Instantiate(mazeCellPrefab, new Vector3(startingPosition.x + i, startingPosition.y - j, startingPosition.z), Quaternion.identity);
            }
        }
        GenerateMaze(null, mazeGrid[0,0]);
        Instantiate(finishLine);
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWall(previousCell, currentCell);
        
        new WaitForSeconds(0.05f);

        MazeCell nextCell;
        do{
            nextCell = GetNextUnvisitedCell(currentCell);

            if(nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        }while (nextCell != null);
        //finishLine.gameObject.SetActive(true);
        
        
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1,10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = Mathf.Abs((int)currentCell.transform.position.x - (int) startingPosition.x);
        int y = Mathf.Abs((int)currentCell.transform.position.y - (int) startingPosition.y);
        //mazeWidth = startingPosition.x + mazeWidth;

        if(x+1 < mazeWidth)
        {
            var cellToRight = mazeGrid[x+1,y];
            if(cellToRight.isVistited == false)
            {
                yield return cellToRight;
            }
        }
        if(x-1 >= 0)
        {
            var cellToLeft = mazeGrid[x-1,y];
            if(cellToLeft.isVistited == false)
            {
                yield return cellToLeft;
            }
        }
        if(y+1 < mazeDepth)
        {
            var cellToFront = mazeGrid[x,y+1];
            if(cellToFront.isVistited == false)
            {
                yield return cellToFront;
            }
        }
        if(y-1 >= 0)
        {
            var cellToBack = mazeGrid[x,y-1];
            if(cellToBack.isVistited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWall(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if(previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if(previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if(previousCell.transform.position.y < currentCell.transform.position.y)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if(previousCell.transform.position.y > currentCell.transform.position.y)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    public void DestroyMaze()
    {
        for(int i = 0; i < mazeWidth; i++)
        {
            for(int j = 0; j < mazeDepth; j++)
            {
                mazeGrid[i,j].ClearBackWall();
                mazeGrid[i,j].ClearFrontWall();
                mazeGrid[i,j].ClearRightWall();
                mazeGrid[i,j].ClearLeftWall();
                mazeGrid[i,j].ClearMazeFloor();
            }
        }
        finishLineToDestroy = GameObject.FindGameObjectWithTag("Finish Line");
        Destroy(finishLineToDestroy);
        Debug.Log("Maze Destroyed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
