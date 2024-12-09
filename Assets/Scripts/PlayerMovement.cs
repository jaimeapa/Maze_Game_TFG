using System;
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
    public GameObject enemy;
    public RaycastReticula raycastReticula; 
    public bool playEnemy = false;
    public float enemySpeed;
    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        reticula = GameObject.Find("Reticula");
        raycastReticula = GameObject.Find("Main Camera").GetComponent<RaycastReticula>();
        try{
            timer = GameObject.Find("Timer").GetComponent<Timer>();
            timer.gameObject.SetActive(false);
        }catch (NullReferenceException e)
        {
            Debug.LogError("Error finding timer: " + e.Message);
        }
        
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

        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        // Instancia el jugador
        spawnManager.SpawnPlayer(startingPos);
        menu.gameObject.SetActive(false);
        raycastReticula.ClearVisitedCells();
        raycastReticula.startPlaying = false;
        scoreText.gameObject.SetActive(true);
        
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
        mazeGenerator.DestroyMaze();
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
        Debug.Log("Restart Button appearing...");
        isPlaying = true;
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
