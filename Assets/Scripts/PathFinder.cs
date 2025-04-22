using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class PathFinder : MonoBehaviour
{
    public MazeCell[,] maze;
    public MazeCell startingPos;
    public MazeCell endPos;
    public int mazeWidth, mazeDepth;
    public MazeGenerator generator;
    //public TextMeshProUGUI message;


    public void Start()
    {
        //generator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        //message = GameObject.Find("Debug").GetComponent<TextMeshProUGUI>();
    }

    public List<MazeCell> FindPath(MazeCell start)
    {
        this.startingPos = start;
        
        maze = generator.getMazeGrid();
        mazeWidth = generator.getMazeWidth();
        mazeDepth = generator.getMazeDepth();
        this.endPos = maze[mazeWidth - 1,mazeDepth - 1];

        HashSet<MazeCell> visited = new HashSet<MazeCell>();
        List<MazeCell> path = new List<MazeCell>();
        
        bool found = DFS(start, path, visited);
        string pathText = "Found: " + found + "Path: ";
        for (int i = 0; i < path.Count; i++)
        {
            pathText = pathText + "\n" + path[i].x + "," + path[i].y;
        }
        //message.text = pathText;
        return found ? path : null;
    }

    private bool DFS(MazeCell current, List<MazeCell> path, HashSet<MazeCell> visited)
    {
        visited.Add(current);
        path.Add(current);

        if (current == endPos)
            return true;

        var neighbors = GetAccessibleNeighbors(current);
        foreach (var neighbor in neighbors)
        {
            if (!visited.Contains(neighbor))
            {
                if (DFS(neighbor, path, visited))
                    return true;
            }
        }

        path.Remove(current); // backtrack
        return false;
    }

    public List<MazeCell> GetAccessibleNeighbors(MazeCell currentCell)
    {
        List<MazeCell> neighbors = new List<MazeCell>();

        if (currentCell.x - 1 >= 0 && !currentCell.HasLeftWall())
            neighbors.Add(maze[currentCell.x - 1, currentCell.y]);

        if (currentCell.x + 1 < mazeWidth && !currentCell.HasRightWall())
            neighbors.Add(maze[currentCell.x + 1, currentCell.y]);

        if (currentCell.y + 1 < mazeDepth && !currentCell.HasBackWall())
            neighbors.Add(maze[currentCell.x, currentCell.y + 1]);

        if (currentCell.y - 1 >= 0 && !currentCell.HasFrontWall())
            neighbors.Add(maze[currentCell.x, currentCell.y - 1]);

        return neighbors;
    }
}