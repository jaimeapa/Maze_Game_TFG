using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DropdownMazes : MonoBehaviour
{
    [SerializeField] public TMP_Dropdown dropdownTMP;
    public PlaySavedMaze play;
    // Start is called before the first frame update
    void Start()
    {
        dropdownTMP.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDropdownValueChanged(int index)
    {
        //string opcionSeleccionada = dropdownTMP.options[index].text;
        TMP_Text selectedTextComponent = dropdownTMP.captionText;
        string opcionSeleccionada = selectedTextComponent.text;
        Debug.Log(opcionSeleccionada);
        play.SetFileName(opcionSeleccionada);
    }
    public List<string> GetMazesNames(string path)
    {
       try
        {
            if (Directory.Exists(path))
            {
                string[] archivos = Directory.GetFiles(path);

                return new List<string>(Array.ConvertAll(archivos, Path.GetFileName));
            }
            else
            {
                Debug.LogWarning("El directorio no existe: " + path);
                return new List<string>(); 
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al obtener los nombres de los archivos: " + e.Message);
            return new List<string>();
        }
    }
}

