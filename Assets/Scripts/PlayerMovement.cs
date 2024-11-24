using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3;
    public GameObject reticula;
    public Rigidbody playerRb;
    public bool isPlaying = false;
    public bool restart = false;
    public float wallCounter = 0;
    //public Button button;
    public GameObject menu;
    public Vector3 startingPos = new Vector3(26.5f, 8.16f, 21.19f);
    public GameObject restartButton;
    public GameObject playerPrefab;
    public SpawnManager spawnManager;
    //public Movement movement;
    public TextMeshProUGUI scoreText;
    public MazeGenerator mazeGenerator;

    // Start is called before the first frame update
    void Start()
    {
        reticula = GameObject.Find("Reticula");
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(restartButton == null)
        {
            restartButton = GameObject.Find("RestartButton");
        }*/
        if (restart)
        {
            isPlaying = true;
            restartButton.gameObject.SetActive(true);
        }
    }

    
    
    public void StartGame(int difficulty)
    {
        if(difficulty == 0){
            //startingPos = new Vector3(startingPos.x + 1, startingPos.y - 1, startingPos.z);
            startingPos = new Vector3(28f, 7f, 17.7f);
        }
        if(difficulty == 1)
        {
            startingPos = new Vector3(26.5f, 8.16f, 21.19f);
        }
        if(difficulty == 2)
        {
            startingPos = new Vector3(startingPos.x - 1, startingPos.y + 2, startingPos.z);
        }

        spawnManager.SpawnPlayer(startingPos);
        isPlaying = true;
        menu.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        restart = false;
        restartButton.gameObject.SetActive(false);
        mazeGenerator.DestroyMaze();
        menu.gameObject.SetActive(true);
        wallCounter =  0;
        scoreText.text = "Score: " + wallCounter;
    }

    public void UpdateScore()
    {
        wallCounter +=1;
        scoreText.text = "Score: " + wallCounter;
    }

    public void Restart()
    {
        Debug.Log("Restart Button appearing...");
        isPlaying = true;

        restartButton.gameObject.SetActive(true);
    }
   
}
