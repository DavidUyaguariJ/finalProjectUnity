using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int puntuacion = 0;
    public TextMeshProUGUI textoPuntaje;

    public GameObject panelVictoria;
    public Button botonReiniciar;
    public TextMeshProUGUI textoBoton;

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

    void Start()
    {
        if (panelVictoria != null)
            panelVictoria.SetActive(false);

        if (botonReiniciar != null)
            botonReiniciar.onClick.AddListener(ReiniciarNivel);

        if (textoBoton != null)
            textoBoton.text = "Reiniciar Nivel";

        ActualizarUI();
    }

    public void AumentarPuntaje(int puntos)
    {
        puntuacion += puntos;
        ActualizarUI();

        if (puntuacion >= 5)
        {
            MostrarVictoria();
        }
    }

    void ActualizarUI()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntuacion.ToString();
        }
    }

    void MostrarVictoria()
    {
        if (panelVictoria != null)
            panelVictoria.SetActive(true);
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
