using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelection : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Button button;
    public MazeGenerator mazeGenerator;
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        mazeGenerator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void SetDifficulty()
    {
        Debug.Log("Start Playing");
        mazeGenerator.CreateMaze(difficulty);
        //string fileName = "2024-10-31-08-55-1";
        //string fileName = "prueba";
        //string fullPath = Application.dataPath + "/MazesSaved/" + fileName + ".txt";
        //mazeGenerator.CreateMazeFromFile(fullPath);
        new WaitForSeconds(0.05f);
        playerMovement.StartGame(difficulty);
        
    }
}
