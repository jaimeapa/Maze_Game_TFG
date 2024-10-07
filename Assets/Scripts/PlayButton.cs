using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Button button;
    public MazeGenerator mazeGenerator;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PressButton);
        mazeGenerator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        //playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressButton()
    {
        Debug.Log("Start Playing");
        mazeGenerator.CreateMaze();
        new WaitForSeconds(0.05f);
        playerMovement.StartGame();
        
    }
}
