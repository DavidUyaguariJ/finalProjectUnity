using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public TextMeshProUGUI textoInteraccion;
    public GameObject rampa;
    public Transform posicionEsfera;
    public float tiempoBajada = 10f;
    private bool jugadorCerca = false;
    private bool rampaActiva = false;
    private Vector3 posicionInicialRampa;

    void Start()
    {
        textoInteraccion.text = "";
        posicionInicialRampa = rampa.transform.position;
    }

    void Update()
    {
        if (jugadorCerca && !rampaActiva)
        {
            textoInteraccion.text = "Presiona E para bajar rampa";

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameManager.instancia.puntuacion >= 4)
                {
                    StartCoroutine(BajarRampa());
                }
                else
                {
                    StartCoroutine(MostrarMensajeTemporal("Necesitas al menos 5 puntos"));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            textoInteraccion.text = "";
        }
    }

    private IEnumerator BajarRampa()
    {
        rampaActiva = true;
        textoInteraccion.text = "";

        rampa.transform.position = new Vector3(
            rampa.transform.position.x,
            posicionEsfera.position.y,
            rampa.transform.position.z
        );

        yield return new WaitForSeconds(tiempoBajada);

        rampa.transform.position = posicionInicialRampa;

        rampaActiva = false;
    }

    private IEnumerator MostrarMensajeTemporal(string mensaje)
    {
        textoInteraccion.text = mensaje;
        yield return new WaitForSeconds(2.5f);
        textoInteraccion.text = "";
    }
}
