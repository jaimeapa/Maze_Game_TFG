using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaySavedMaze : MonoBehaviour
{
    public Button button;
    public string filename = "";
    public MazeGenerator mazeGenerator;
    public PlayerMovement playerMovement;
    public GameObject menu;
    public TextMeshProUGUI error;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Play);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        Debug.Log("Start Playing");
        new WaitForSeconds(0.05f);
        int difficulty = mazeGenerator.CreateMazeFromFile(filename);
        if (difficulty == -1)
        {
            error.text = "Error getting maze from file: " + filename;
        }else{
            error.text = "";
            menu.gameObject.SetActive(false);
            playerMovement.StartGame(difficulty);
        }
        
        
        //playerMovement.StartGame(difficulty);
        
    }
    public void SetFileName(string name)
    {
        filename = name;
    }
}
