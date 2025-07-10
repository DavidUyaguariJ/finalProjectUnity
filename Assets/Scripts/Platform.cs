using System.Collections;
using TMPro;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public TextMeshProUGUI textoInteraccion;
    public GameObject rampa;
    public Transform posicionEsfera;
    public float tiempoBajada = 10f;
    public float duracionMovimiento = 1f;

    private bool jugadorCerca = false;
    private bool rampaActiva = false;
    private Vector3 posicionInicialRampa;

    private void Start()
    {
        textoInteraccion.text = "";
        posicionInicialRampa = rampa.transform.position;
    }

    private void Update()
    {
        if (jugadorCerca && !rampaActiva)
        {
            textoInteraccion.text = "Presiona E para bajar la rampa";

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameManager.instancia.puntuacion >= 4)
                {
                    StartCoroutine(BajarYSubirRampa());
                }
                else
                {
                    StartCoroutine(MostrarMensajeTemporal("Necesitas al menos 4 orbes"));
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

    private IEnumerator BajarYSubirRampa()
    {
        rampaActiva = true;
        textoInteraccion.text = "";

        Vector3 posicionBajada = new Vector3(
            rampa.transform.position.x,
            posicionEsfera.position.y-5f,
            rampa.transform.position.z
        );

        // Bajar suavemente
        yield return StartCoroutine(MoverRampa(rampa.transform.position, posicionBajada));

        // Esperar abajo
        yield return new WaitForSeconds(tiempoBajada);

        // Subir suavemente
        yield return StartCoroutine(MoverRampa(rampa.transform.position, posicionInicialRampa));

        rampaActiva = false;
    }

    private IEnumerator MoverRampa(Vector3 desde, Vector3 hasta)
    {
        float tiempo = 0f;

        while (tiempo < duracionMovimiento)
        {
            rampa.transform.position = Vector3.Lerp(desde, hasta, tiempo / duracionMovimiento);
            tiempo += Time.deltaTime;
            yield return null;
        }

        rampa.transform.position = hasta;
    }

    private IEnumerator MostrarMensajeTemporal(string mensaje)
    {
        textoInteraccion.text = mensaje;
        yield return new WaitForSeconds(2.5f);
        textoInteraccion.text = "";
    }
}
