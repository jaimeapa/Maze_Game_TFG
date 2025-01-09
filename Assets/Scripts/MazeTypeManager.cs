using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MazeTypeManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    public MazeGenerator mazeGenerator;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

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
