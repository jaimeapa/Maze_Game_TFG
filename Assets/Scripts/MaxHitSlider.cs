using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaxHitSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    private String text;
    private PlayerMovement playerMovement;


    public void OnSliderChange(float value)
    {
        playerMovement = GameObject.Find("Manager").GetComponent<PlayerMovement>();
        text = "Hits: " + (int)value + " hits";
        textUI.text = text;
        playerMovement.SetMaxHits((int)value);
    }
}
