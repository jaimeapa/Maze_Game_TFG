using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavedMazesButton : MonoBehaviour
{
    public Button button;
    public GameObject menu;
    public GameObject mazesSavedMenu;
    public DropdownMazes dropdownMazes;
    public TMP_Dropdown dropdownTMP;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PressButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PressButton()
    {
        mazesSavedMenu.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
        List<string> list = new List<string>();
        string[] value = { "0", "0", "0", "0", "0", "0"};
        string[] name = value;
        //string finalName;
        //int i = 1;
        try{
            List<string> mazes = dropdownMazes.GetMazesNames(Application.persistentDataPath);
            /*foreach(string mazesName in mazes)
            {
                string[] numberOfDate = mazesName.Split("-");
                if(numberOfDate[0] == name[0] && numberOfDate[1] == name[1] && numberOfDate[2] == name[2]){
                    i++;
                    finalName = numberOfDate[0] + "/" + numberOfDate[1] + "/" + numberOfDate[2] + "(" + i + ")";
                }else{
                    i = 1;
                    finalName = numberOfDate[0] + "/" + numberOfDate[1] + "/" + numberOfDate[2];
                }
                list.Add(finalName);
            }*/
            dropdownTMP.ClearOptions();
            dropdownTMP.AddOptions(mazes);
        }catch(NullReferenceException e){
            Debug.Log(e.ToString());
        }
        
    }
}
