using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
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
    public Vector3 startingPos = new Vector3(26.5f, 8.16f, 21f);
    public GameObject restartButton;
    public GameObject playerPrefab;
    public SpawnManager spawnManager;
    //public Movement movement;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI time;
    public TextMeshProUGUI data;
    public TextMeshProUGUI error;
    public TextMeshProUGUI start;
    public TextMeshProUGUI instructions;
    private bool startPlaying = false;
    public MazeGenerator mazeGenerator;
    public GameObject enemy;
    public RaycastReticula raycastReticula; 
    public bool playEnemy = false;
    public float enemySpeed;
    public Timer timer;
    public bool showTime = false;
    public Stopwatch stopwatch;
    public int timeTaken;
    public Report report;
    public float actionRad;

    // Start is called before the first frame update
    void Start()
    {
        reticula = GameObject.Find("Reticula");
        raycastReticula = GameObject.Find("Main Camera").GetComponent<RaycastReticula>();
        enemySpeed = 0.5f;
        try{
            timer = GameObject.Find("Timer").GetComponent<Timer>();
            timer.gameObject.SetActive(false);
        }catch (NullReferenceException e)
        {
            Debug.LogError("Error finding timer: " + e.Message);
        }
        if(spawnManager == null){
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        }
        if(menu == null)
        {
            menu = GameObject.Find("DifficultySelection");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            isPlaying = true;
            restartButton.gameObject.SetActive(true);
        }
        if(showTime)
        {
            time.text = "Time: " + stopwatch.getActualTime();
        }
        if(raycastReticula.startPlaying && !startPlaying)
        {
            startPlaying = true;
            StartCoroutine(StartPlaying());
        }
        if (raycastReticula.startPlaying)
        {
            instructions.gameObject.SetActive(false);
        }
    }

    public IEnumerator StartPlaying()
    {
        start.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        
        start.gameObject.SetActive(false);  
    }
    
    public void StartGame(int difficulty)
    {
        if(difficulty == 0){
            startingPos = new Vector3(28f, 7f, 17.5f);
        }
        if(difficulty == 1)
        {
            startingPos = new Vector3(26.5f, 8.16f, 21f);
        }
        if(difficulty == 2)
        {
            startingPos = new Vector3(startingPos.x - 1, startingPos.y + 2, startingPos.z);
        }
        
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Look at the character to start";
        // Instancia el jugador
        spawnManager.SpawnPlayer(startingPos);
        menu.gameObject.SetActive(false);
        raycastReticula.ClearVisitedCells();
        raycastReticula.startPlaying = false;
        scoreText.gameObject.SetActive(true);
        stopwatch.Start();
        if(showTime)
        {
            time.gameObject.SetActive(true);
        }
        
        if(playEnemy){
            Debug.Log("Iniciar Temporizador...");
            timer.gameObject.SetActive(true);
            timer.IniciarTemporizador();
            Debug.Log("Temporizador terminado");
            yield return new WaitForSeconds(5);
            spawnManager.SpawnEnemy(startingPos);
        }
        
        isPlaying = true;
        
        
}
    public void RestartGame()
    {
        restart = false;
        restartButton.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
        wallCounter =  0;
        scoreText.text = "Score: " + wallCounter;
    }

    public void UpdateScore()
    {
        wallCounter +=1;
        scoreText.text = "Score: " + (int)wallCounter/2;
    }

    public void Restart()
    {
        timeTaken = stopwatch.Stop();
        Debug.Log(timeTaken);
        scoreText.gameObject.SetActive(false);
        time.gameObject.SetActive(false);
        int wallsHit = (int)wallCounter/2;
        report.setTime(timeTaken);
        report.setWalls(wallsHit);
        data.text = "Time: " + timeTaken + "s\nWall Hits: " + wallsHit;  
        Debug.Log("Restart Button appearing...");
        isPlaying = true;
        mazeGenerator.DestroyMaze();
        restartButton.gameObject.SetActive(true);
    }

    public Vector3 GetStartingPos(){
        return startingPos;
    }
    public void SetEnemySpeed(float value)
    {
        enemySpeed = value;
    }
    public float GetEnemySpeed()
    {
        return enemySpeed;
    }
   
}
