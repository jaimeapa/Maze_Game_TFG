using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
   //public float tiempoInicial = 60f; // Tiempo inicial en segundos.
    private float tiempoRestante;
    [SerializeField] private TextMeshProUGUI textUI;
    private Coroutine temporizadorCoroutine;

     public void IniciarTemporizador()
    {
        //textUI.gameObject.SetActive(true);
        if (temporizadorCoroutine != null)
        {
            StopCoroutine(temporizadorCoroutine); // Detener cualquier temporizador en curso.
        }
        temporizadorCoroutine = StartCoroutine(TemporizadorCoroutine(5f)); // Duración de 5 segundos.
    }

    // Método para detener el temporizador
    public void DetenerTemporizador()
    {
        if (temporizadorCoroutine != null)
        {
            StopCoroutine(temporizadorCoroutine);
            temporizadorCoroutine = null;
        }
        if (textUI != null)
        {
            textUI.text = Mathf.CeilToInt(tiempoRestante).ToString();
        }
    }

    // Corrutina para manejar los 5 segundos
    private IEnumerator TemporizadorCoroutine(float duracion)
    {
        float tiempoRestante = duracion;

        while (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            //int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            //int segundos = Mathf.FloorToInt(tiempoRestante % 60);

            if (textUI != null)
            {
                textUI.text = Mathf.CeilToInt(tiempoRestante).ToString();
            }

            yield return null; // Esperar al siguiente frame.
        }

        if (textUI != null)
        {
            textUI.text = "00:00";
        }

        // Lógica al terminar el temporizador.
        Debug.Log("¡El temporizador de 5 segundos ha terminado!");
        textUI.gameObject.SetActive(false);
    }
}
