using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Button button;
    public MazeGenerator mazeGenerator;
    private int mazeNumber = 0;
    public GameObject menu;
    public GameObject playButton;
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
        mazeNumber += 1;
        mazeGenerator.mazeNumber = mazeNumber;
        //mazeGenerator.CreateMaze(1);
        //string fileName = "2024-10-31-08-55-1";
        //string fileName = "prueba";
        //string fullPath = Application.dataPath + "/MazesSaved/" + fileName + ".txt";
        //mazeGenerator.CreateMazeFromFile(fullPath);
        new WaitForSeconds(0.05f);
        playButton.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
        //playerMovement.StartGame();
        
    }
}
