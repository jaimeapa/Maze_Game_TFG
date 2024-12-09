using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    //[SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI textUI;
    private String text;
    private PlayerMovement playerMovement;

   
    public void OnSliderChange (float value){
        playerMovement = GameObject.Find("Manager").GetComponent<PlayerMovement>();
        //speed = value;
        text = "Speed: " + 10*value/1.5;
        textUI.text = text;
        playerMovement.SetEnemySpeed(value);
    }
    
}
