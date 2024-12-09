using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void SpawnPlayer(Vector3 pos)
    {
        Instantiate(playerPrefab, pos, playerPrefab.transform.rotation);
    }
    public void SpawnEnemy(Vector3 pos){
        Instantiate(enemyPrefab, pos, enemyPrefab.transform.rotation);
        Debug.Log("Enemy spawned");
    }
}
