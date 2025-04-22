using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private float[] startTime = new float[5]; // Almacena el tiempo en que se inicia el cronómetro.
    private bool activeStopwatch = false; // Controla si el cronómetro está activo.
    private int [] time = new int[5]; // Tiempo transcurrido en segundos como entero.

    // Función para iniciar el cronómetro.
    public void StartStopwatch(int i)
    {
        startTime[i] = Time.time; 
        activeStopwatch = true;
        time[i] = 0; 
    }

    public int Stop(int i)
    {
        if (activeStopwatch)
        {
            time[i] = Mathf.FloorToInt((Time.time - startTime[i]) * 1000); 
            activeStopwatch = false;
        }
        else
        {
            Debug.LogWarning("Stopwatch is not active");
        }

        return time[i];
    }

    public int getActualTime(int i)
    {
        if (activeStopwatch)
        {
            return Mathf.FloorToInt(Time.time - startTime[i]);
        }
        else
        {
            return 0;
        }

        //return time[i]; 
    }
}
