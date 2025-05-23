using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RaycastReticula : MonoBehaviour
{
    //public GameObject corona;
    public GameObject reticula;
    private float amount = 3;
    private float timer = 0;
    private List<MazeCell> visitedCells = new List<MazeCell>();
    public GameObject enemy;
    public bool startPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            if(hit.transform.gameObject.tag == "Maze Floor")
            {
                reticula.gameObject.SetActive(true);
                ReticulaAdaptation(hit.distance, hit.normal);

                //Guardar las celdas que visita el jugador
                //var mazeCell = hit.transform.GetComponent<MazeCell>();
                MazeCell mazeCell = hit.transform.GetComponentInParent<MazeCell>();
                if(mazeCell.x == 0 && mazeCell.y == 0){
                    startPlaying = true;
                }
                if (mazeCell != null && !mazeCell.visitedByPlayer && startPlaying)
                {
                    mazeCell.MarkAsVisited();
                    visitedCells.Add(mazeCell); 
                }

            }else{
                if(hit.transform.gameObject.tag == "Wall")
                {
                    reticula.gameObject.SetActive(true);
                    ReticulaAdaptation(hit.distance + 1, hit.normal);
                }else{
                    reticula.gameObject.SetActive(false);
                }
                
            }
        }
        else
        {
            timer = 0;
            reticula.gameObject.SetActive(false);
            ReticulaAdaptation(20, -transform.forward);
        }
        /*Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 10))
            print("There is something in front of the object!");
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            print("Found an object - distance: " + hit.distance);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
            Debug.DrawLine(ray.origin, hit.point);*/
    }

    private void ReticulaAdaptation (float distancia, Vector3 normal)
    {
        Vector3 posicionReticula = reticula.transform.localPosition;
        posicionReticula.z = distancia - 0.1f;

        reticula.transform.localPosition = posicionReticula;

        //rotar el objeto de manera que se quede mirando a una direccion, que es la superficie
        Vector3 offset = new Vector3(0,90,0);
        reticula.transform.rotation = Quaternion.LookRotation(normal + offset);
    }
    public List<MazeCell> GetVisitedCells()
    {
        return visitedCells;
    }
    public void ClearVisitedCells(){
        visitedCells.Clear();
    }
}
