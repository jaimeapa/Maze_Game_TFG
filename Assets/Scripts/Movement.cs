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
        speed = 1f;   
        playerMovement = GameObject.Find("Manager").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(reticula == null)
        {
            try{
                reticula = GameObject.Find("Reticula");
            }catch(NullReferenceException e){
                Debug.Log("No se encuentra retícula"+ e.Message);
            }
        }
        /*Vector3 direction = (reticula.transform.position - playerRb.transform.position).normalized;
        float x = direction.x;
        float y = direction.y;
        float distance = Vector3.Distance(transform.position, reticula.transform.position);
        if(distance <= 2.5){
            playerRb.AddForce(new Vector2(x,y) * speed);
        }*/
        if (reticula != null)
        {
            Vector3 direction = (reticula.transform.position - playerRb.transform.position).normalized;
            float distance = Vector3.Distance(playerRb.transform.position, reticula.transform.position);

            if (distance > 0.1f && distance <= 2.5f) 
            {
                playerRb.velocity = direction * speed;
            }else{
                 playerRb.velocity = Vector3.zero;
            }
    }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        /*if(collision.gameObject.CompareTag("Finish Line"))
        {
            Debug.Log("Collision con Finish line");
            restart = true;
            Destroy(gameObject);
            Debug.Log("You Win!");
            playerMovement.Restart();
            //playerMovement.isPlaying = false;
            //playerMovement.restart = true;
            //restartButton.gameObject.SetActive(true);
        }*/
        if(collision.gameObject.CompareTag("Wall"))
        {
            playerMovement.UpdateScore();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger con Finish line");
        if(collision.gameObject.CompareTag("Finish Line"))
        {
            restart = true;
            Destroy(gameObject);
            Debug.Log("You Win!");
            
            playerMovement.Restart();
            //playerMovement.isPlaying = false;
            //playerMovement.restart = true;
            //restartButton.gameObject.SetActive(true);
        }
    }
}

