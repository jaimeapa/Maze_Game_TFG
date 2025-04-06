using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject reticula;
    private Rigidbody playerRb;
    private float speed;
    public float wallCounter = 0;
    public bool restart = false;
    public PlayerMovement playerMovement;
    public RaycastReticula raycastReticula; 
    public GameObject enemy;
    private bool enemyFound = false;
    public float actionRad = 2.5f;
    public float rotationSpeed = 1f;
    public Transform playerTransform;
    public Stopwatch stopwatch;
    public int timeWallHit;
    public bool inMaze;
    private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        restart = false;
        playerRb = GetComponent<Rigidbody>();
        try{
            reticula = GameObject.Find("Reticula");
        }catch(NullReferenceException e){
            Debug.Log("No se encuentra retícula"+ e.Message);
        }
        speed = 1.5f;   
        playerMovement = GameObject.Find("Manager").GetComponent<PlayerMovement>();
        raycastReticula = GameObject.Find("Main Camera").GetComponent<RaycastReticula>();
        stopwatch = GameObject.Find("Stopwatch").GetComponent<Stopwatch>();
        playerTransform = playerRb.transform;
        timeWallHit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(reticula == null)
        {
            try{
                reticula = GameObject.FindWithTag("Reticula");
            }catch(NullReferenceException e){
                Debug.Log("No se encuentra retícula"+ e.Message);
            }
        }
        if (!enemyFound && enemy == null)
        {
            try
            {
                enemy = GameObject.FindWithTag("Enemy");
                if (enemy != null)
                {
                    Debug.Log("Enemy found");
                    enemyFound = true; // Evita que se ejecute nuevamente
                }
                
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("Error finding enemy: " + e.Message);
            }
        }


        if (reticula != null && raycastReticula.startPlaying)
        {

            float startPos = playerMovement.startingPos.z;

            float distance = Vector3.Distance(playerRb.transform.position, reticula.transform.position);
            if(playerMovement.actionRad != null)
            {
                float actionRad = playerMovement.actionRad;
            }
            else
            {
                actionRad = 2.5f;
            }
            
            // Aplicar la rotación en el eje Z del mundo
            
            if (distance > 0.1f && distance <= actionRad && inMaze)
            {

                Vector3 direction = (reticula.transform.position - playerRb.transform.position).normalized;
                float angleX = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;
                targetRotation = Quaternion.Euler(angleX, playerRb.transform.rotation.y + 90, playerRb.transform.rotation.z - 90);
                
                playerRb.MoveRotation(targetRotation);

                // Mover el personaje en la dirección correcta (evita que se mueva en Y si no queremos flotación)

                playerRb.velocity = new Vector3(direction.x, direction.y, startPos) * speed;
            }
            else
            {
                playerRb.velocity = new Vector3(playerRb.transform.position.x, playerRb.transform.position.y, startPos) * speed;
                playerRb.velocity = Vector3.zero;
                playerRb.MoveRotation(targetRotation);
            }
        }


        /*if (reticula != null && raycastReticula.startPlaying)
        {
            Vector3 direction = (reticula.transform.position - playerRb.transform.position).normalized;

            float angleX = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;

            //playerRb.transform.rotation = targetRotation;
            float distance = Vector3.Distance(playerRb.transform.position, reticula.transform.position);
            actionRad = playerMovement.actionRad;
            if (distance > 0.1f && distance <= actionRad)
            {
                Quaternion targetRotation = Quaternion.Euler(angleX, playerRb.transform.rotation.eulerAngles.y, playerRb.transform.rotation.eulerAngles.z);
                playerRb.MoveRotation(targetRotation);
                playerRb.velocity = direction * speed;
            }
            else
            {
                playerRb.velocity = Vector3.zero;
            }
        }*/
        /*if (reticula != null && raycastReticula.startPlaying)
        {
            Vector3 direction = (reticula.transform.position - playerRb.transform.position).normalized;
            float distance = Vector3.Distance(playerRb.transform.position, reticula.transform.position);

            actionRad = playerMovement.actionRad;
            if (distance > 0.1f && distance <= actionRad)
            {
                playerRb.velocity = direction * speed;

            // Asegurarse de que el objeto gire hacia la dirección del movimiento
                if (direction != Vector3.zero) // Evitar errores con un vector de dirección cero
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    playerRb.transform.rotation = Quaternion.Slerp(playerRb.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
            }
            else
            {
                playerRb.velocity = Vector3.zero;
            }
        }*/
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Maze Floor") || collision.gameObject.CompareTag("Wall"))
        {
            inMaze = true;
        }
        else
        {
            inMaze = false;
        }
        
        if(collision.gameObject.CompareTag("Wall") && raycastReticula.startPlaying)
        {
            playerMovement.UpdateScore();
            stopwatch.StartStopwatch(1);
        }
        else
        {
            timeWallHit = timeWallHit + stopwatch.Stop(1);
            playerMovement.SetWallTime(timeWallHit);
            if(collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                Debug.Log("You got caught!");
                playerMovement.Restart();
            }
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger con Finish line");
        if(collision.gameObject.CompareTag("Finish Line"))
        {
            restart = true;
            Destroy(gameObject);
            Destroy(enemy);
            Debug.Log("You Win!");
            
            playerMovement.Restart();
            //playerMovement.isPlaying = false;
            //playerMovement.restart = true;
            //restartButton.gameObject.SetActive(true);
        }
        
    }
}

