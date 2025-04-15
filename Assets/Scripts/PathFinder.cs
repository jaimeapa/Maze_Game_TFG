using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public MazeCell[,] maze;
    public MazeCell[,] path;
    public MazeCell startingPos;
    public int mazeWidth, mazeDepth;
    public MazeGenerator generator;
    public void Start()
    {
        generator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
    }

    public List<MazeCell> FindPath(MazeCell cell)
    {
        maze = generator.getMazeGrid();
        mazeWidth = generator.getMazeWidth();
        mazeDepth = generator.getMazeDepth();
        List<MazeCell> alternative = new List<MazeCell>();
        alternative.Add(startingPos);
        MazeCell currentCell = alternative[0];
        MazeCell nextCell = null;
        int i = 0;
        while (i < 1000)
        {
            do
            {
                nextCell = GetNextUnvisitedCell(currentCell);
                if(nextCell != null)
                {
                    alternative.Add(nextCell);
                    currentCell = nextCell;
                }
            } while(nextCell != null);

            if(alternative.Last().x == mazeWidth && alternative.Last().y == mazeDepth)
            {
                return alternative;
            }
            i++;
        }
        return null;
    }

    
    public MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetAccessibleNeighbors(currentCell);
        return unvisitedCells.OrderBy(_ => UnityEngine.Random.Range(1, 10)).FirstOrDefault();
    }
    public List<MazeCell> GetAccessibleNeighbors(MazeCell currentCell)
    {
        List<MazeCell> neighbors = new List<MazeCell>();

        if (currentCell.x - 1 >= 0 && !currentCell.HasLeftWall())
            neighbors.Add(maze[currentCell.x - 1, currentCell.y]);

        if (currentCell.x + 1 < mazeWidth && !currentCell.HasRightWall())
            neighbors.Add(maze[currentCell.x + 1, currentCell.y]);

        if (currentCell.y + 1 < mazeDepth && !currentCell.HasFrontWall())
            neighbors.Add(maze[currentCell.x, currentCell.y + 1]);

        if (currentCell.y - 1 >= 0 && !currentCell.HasBackWall())
            neighbors.Add(maze[currentCell.x, currentCell.y - 1]);

        foreach (var neighbor in neighbors)
        {
            Debug.Log($"Accessible neighbor: {neighbor.x}, {neighbor.y}");
        }
        return neighbors;
    }
}
