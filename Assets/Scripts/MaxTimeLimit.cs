using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MaxTimeLimit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    private String text;
    private PlayerMovement playerMovement;


    public void OnSliderChange (float value)
    {
        playerMovement = GameObject.Find("Manager").GetComponent<PlayerMovement>();
        string formattedFloat = value.ToString("F2");
        text = "Time: " + formattedFloat + " s";
        textUI.text = text;
        playerMovement.SetMaxTime(value);
    }
}
