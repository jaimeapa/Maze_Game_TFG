using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System;
using System.IO;

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
    public Vector3 startingPosition = new Vector3(26.5f, 8.16f,20.64f);
    public Vector3 finishPos;
    private String maze;
    public int mazeNumber = 0;
    public int mazeShape = 0;
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        
    } 
    
    public void CreateMaze(int difficulty)
    {
        if(difficulty == 0)
        {
            mazeDepth = 6;
            mazeWidth = 6;
            //startingPosition = new Vector3(startingPosition.x + 1, startingPosition.y - 1, startingPosition.z);
            startingPosition = new Vector3(28f,7f,17f);
        }else{
            if(difficulty == 2)
            {
                mazeDepth = 10;
                mazeWidth = 10;
                startingPosition = new Vector3(startingPosition.x - 1, startingPosition.y + 2, startingPosition.z);
            }else 
            {
                mazeDepth = 8;
                mazeWidth = 8;
                startingPosition = new Vector3(26.5f, 8.16f,20.64f);
            }
        }
        finishPos = new Vector3(startingPosition.x + mazeWidth - 0.8f, startingPosition.y - mazeDepth + 1, startingPosition.z + 0.8f);
        maze =  mazeWidth + "\n" + mazeDepth + "\n";
        Debug.Log("Maze is Being created");
        mazeGrid = new MazeCell[mazeWidth, mazeDepth];

        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeDepth; j++)
            {
                mazeGrid[i,j] = Instantiate(mazeCellPrefab, new Vector3(startingPosition.x + i, startingPosition.y - j, startingPosition.z), Quaternion.identity);
                mazeGrid[i,j].x = i;
                mazeGrid[i,j].y = j;
            }
        }
        GenerateMaze(null, mazeGrid[0,0]);
        Instantiate(finishLine, finishPos, finishLine.transform.rotation);
        /*String fileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm");

        string fullPath = Application.dataPath + "/MazesSaved/" + fileName + ".txt";
        Debug.Log(fullPath);
        File.WriteAllText(fullPath, maze);
        maze = "";*/
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
        //DateTime date = DateTime.Now;
                
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        
        if(mazeShape == 1)
        {
            var unvisitedCells = GetUnvisitedCells(currentCell).ToList();
            
             if (unvisitedCells.Count == 0)
                return null;

            var weightedCells = new List<MazeCell>();
            foreach (var cell in unvisitedCells)
            {
                bool isVertical = Mathf.Abs((int)cell.transform.position.y - (int)currentCell.transform.position.y) > 0;
                int weight = isVertical ? 3 : 1;
                for (int i = 0; i < weight; i++)
                {
                    weightedCells.Add(cell);
                }
                
            }
            return weightedCells[UnityEngine.Random.Range(0, weightedCells.Count)];
        }
        else
        {
            if(mazeShape == 2)
            {
                var unvisitedCells1 = GetUnvisitedCells(currentCell).ToList();
            
                if (unvisitedCells1.Count == 0)
                    return null;

                var weightedCells = new List<MazeCell>();
                foreach (var cell in unvisitedCells1)
                {
                    bool isHorizontal = Mathf.Abs((int)cell.transform.position.x - (int)currentCell.transform.position.x) > 0;
                    int weight = isHorizontal ? 3 : 1;
                    for (int i = 0; i < weight; i++)
                    {
                        weightedCells.Add(cell);
                    }
                
                }
                return weightedCells[UnityEngine.Random.Range(0, weightedCells.Count)];
            }else
            {
                var unvisitedCells = GetUnvisitedCells(currentCell);
                return unvisitedCells.OrderBy(_ => UnityEngine.Random.Range(1,10)).FirstOrDefault();
            }
            
        }
        
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = Mathf.Abs((int)currentCell.transform.position.x - (int) startingPosition.x);
        int y = Mathf.Abs((int)currentCell.transform.position.y - (int) startingPosition.y);
        //mazeWidth = startingPosition.x + mazeWidth;

        maze = maze + x + "," + y + "\n";

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

    public void CreateMazeFromFile(string Path)
    {
        string mazefromFile = File.ReadAllText(Path);
        string[ ] linesInFile = mazefromFile.Split("\n");
        int.TryParse(linesInFile[0], out mazeWidth);
        int.TryParse(linesInFile[1], out mazeDepth);
        int[] xcoordenates = new int[linesInFile.Length - 2];
        int[] ycoordenates = new int[linesInFile.Length - 2];
        int length = xcoordenates.Length;
        for (int i = 2;  i < linesInFile.Length; i++)
        {
            string line = linesInFile[i];
            if (!string.IsNullOrWhiteSpace(line))
            {
                //string trimmedLine = line.Trim(new char[] { '[', ']', ' ', '\r' });

                // Dividir en las coordenadas x e y
                string[] partes = line.Split(',');

                if (partes.Length == 2 && int.TryParse(partes[0], out int x) &&int.TryParse(partes[1], out int y))
                {
                    Debug.Log("Coordenada x: " + x + ", y: " + y);
                    xcoordenates[i-2] = x;
                    ycoordenates[i-2] = y;
                }
            }
        }
        Debug.Log(mazeDepth +", "+ mazeWidth);
        mazeGrid = new MazeCell[mazeWidth, mazeDepth];

        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeDepth; j++)
            {
                mazeGrid[i,j] = Instantiate(mazeCellPrefab, new Vector3(startingPosition.x + i, startingPosition.y - j, startingPosition.z), Quaternion.identity);
            }
        }

        for(int i = 0; i < length-2; i++)
        {
            GenerateMazeFromFile(xcoordenates[i], ycoordenates[i], xcoordenates[i+1], ycoordenates[i+1]);
        }
        Debug.Log(xcoordenates[length-2] + ", "+ ycoordenates[length-2] + "\n" + xcoordenates[length-1] + "," + ycoordenates[length-1]);
        GenerateMaze(mazeGrid[xcoordenates[length-2], ycoordenates[length-2]], mazeGrid[xcoordenates[length-1], ycoordenates[length-1]]);
        //GenerateMaze(null, mazeGrid[0,0]);
        Instantiate(finishLine);
    }
    public void GenerateMazeFromFile(int x1, int y1, int x2, int y2)
    {
        MazeCell previousCell = mazeGrid[x1, y1];
        MazeCell currentCell = mazeGrid[x2, y2];
        if(x1 == 0 && y1 == 0)
        {
            previousCell.Visit();
        }
        currentCell.Visit();
        ClearWall(previousCell, currentCell);
        new WaitForSeconds(0.05f);
    }

    public MazeCell[,] getMazeGrid(){
        return this.mazeGrid;
    }
    public int getMazeWidth()
    {
        return this.mazeWidth;
    }
    public int getMazeDepth()
    {
        return this.mazeDepth;  
    }

    public List<MazeCell> GetAccessibleNeighbors(MazeCell currentCell)
    {
         List<MazeCell> neighbors = new List<MazeCell>();

        if (currentCell.x - 1 >= 0 && !currentCell.HasLeftWall())
            neighbors.Add(mazeGrid[currentCell.x - 1, currentCell.y]);

        if (currentCell.x + 1 < mazeWidth && !currentCell.HasRightWall())
            neighbors.Add(mazeGrid[currentCell.x + 1, currentCell.y]);

        if (currentCell.y + 1 < mazeDepth && !currentCell.HasFrontWall())
            neighbors.Add(mazeGrid[currentCell.x, currentCell.y + 1]);

        if (currentCell.y - 1 >= 0 && !currentCell.HasBackWall())
            neighbors.Add(mazeGrid[currentCell.x, currentCell.y - 1]);

        foreach (var neighbor in neighbors)
        {
            Debug.Log($"Accessible neighbor: {neighbor.x}, {neighbor.y}");
        }
        return neighbors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
