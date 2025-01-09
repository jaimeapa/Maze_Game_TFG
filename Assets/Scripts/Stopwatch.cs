using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private float startTime; // Almacena el tiempo en que se inicia el cronómetro.
    private bool activeStopwatch = false; // Controla si el cronómetro está activo.
    private int time = 0; // Tiempo transcurrido en segundos como entero.

    // Función para iniciar el cronómetro.
    public void Start()
    {
        startTime = Time.time; 
        activeStopwatch = true;
        time = 0; 
    }

    public int Stop()
    {
        if (activeStopwatch)
        {
            time = Mathf.FloorToInt(Time.time - startTime); 
            activeStopwatch = false;
        }
        else
        {
            Debug.LogWarning("Stopwatch is not active");
        }

        return time;
    }

    public int getActualTime()
    {
        if (activeStopwatch)
        {
            return Mathf.FloorToInt(Time.time - startTime);
        }

        return time; 
    }
}
