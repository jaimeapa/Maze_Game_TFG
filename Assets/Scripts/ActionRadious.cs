using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ActionRadious : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    private String text;
    private PlayerMovement playerMovement;

   
    public void OnSliderChange (float value){
        playerMovement = GameObject.Find("Manager").GetComponent<PlayerMovement>();
        //speed = value;
        float myFloat = (float)(10 *value/3);
        string formattedFloat = myFloat.ToString("F2");
        text = "Action Radious: " + formattedFloat;
        textUI.text = text;
        playerMovement.actionRad = myFloat;
    }
}
