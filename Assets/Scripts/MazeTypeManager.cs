using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeTypeManager : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    public MazeGenerator mazeGenerator;
    // Start is called before the first frame update
    /*void Start()
    {
        dropdown = GameObject.Find("MazeType").GetComponent<Dropdown>();
        mazeGenerator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnDropdownValueChanged(int index)
    {
        mazeGenerator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        mazeGenerator.mazeShape = index;
        Debug.Log("Maze Type seleccionado: " + index);
    }
}
