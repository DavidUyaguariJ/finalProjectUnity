using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int puntuacion = 0;
    public TextMeshProUGUI textoPuntaje;

    public static GameManager instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AumentarPuntaje(int puntos)
    {
        puntuacion += puntos;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntuacion.ToString();
        }
    }
}