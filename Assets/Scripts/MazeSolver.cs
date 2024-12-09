using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeSolver : MonoBehaviour
{
    private MazeGenerator mazeGenerator;
    private MazeCell[,] mazeGrid;
    private int mazeDepth;
    private int mazeWidth;
    private Vector2 startingPos = new Vector2(0, 0);
    private Vector2 finishPos;
    private List<Vector2> path = new List<Vector2>();
    private List<Vector2> path2 = new List<Vector2>();
    private MazeCell nextCell;
    // Start is called before the first frame update
    void Start()
    {
        mazeGenerator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        mazeGrid = mazeGenerator.getMazeGrid();
        mazeDepth = mazeGenerator.getMazeDepth();
        mazeWidth = mazeGenerator.getMazeWidth();
        finishPos = new Vector2(mazeWidth, mazeDepth);
        for(int i = 0; i < 100; i++){
            SolveMaze(null, mazeGrid[0,0]);
            
        }
    }

    void SolveMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.VisitAgain();
        path.Add(new Vector2(currentCell.x, currentCell.y));
        
        new WaitForSeconds(0.05f);

        MazeCell nextCell;
        do{
            nextCell = GetNextCell(currentCell);

            if(nextCell.x != mazeDepth && nextCell.y != mazeWidth)
            {
                SolveMaze(currentCell, nextCell);
            }
        }while (nextCell.x != mazeDepth && nextCell.y != mazeWidth);
    }

    private MazeCell GetNextCell(MazeCell currentCell){
        var unvisitedCells = GetPossibleCells(currentCell);
        return unvisitedCells.OrderBy(_ => UnityEngine.Random.Range(1,10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetPossibleCells(MazeCell currentCell)
    {
        int x = Mathf.Abs((int)currentCell.transform.position.x - (int) startingPos.x);
        int y = Mathf.Abs((int)currentCell.transform.position.y - (int) startingPos.y);
        if(!currentCell.HasRightWall())
        {
            if(currentCell.isVistited){
                nextCell = mazeGrid[x + 1, y];
                yield return nextCell;
            }
        }
        if(!currentCell.HasLeftWall())
        {
            if(currentCell.isVistited){
                nextCell = mazeGrid[x - 1, y];
                yield return nextCell;
            }
        }
        if(!currentCell.HasBackWall())
        {
            if(currentCell.isVistited){
                nextCell = mazeGrid[x, y + 1];
                yield return nextCell;
            }
        }
        if(!currentCell.HasRightWall())
        {
            if(currentCell.isVistited){
                nextCell = mazeGrid[x, y - 1];
                yield return nextCell;
            }
        }
    }

    public void UnvistCells(int mazeDepth, int mazeWidth, MazeCell[,] mazeGrid)
    {
        for(int i = 0; i < mazeDepth; i++)
        {
            for (int j = 0; j < mazeWidth; j++)
            {
                mazeGrid[i,j].Visit();
            }
        }
    }
}
