using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text vidaText;

    public void ActualizarVidas(int vidas)
    {
        vidaText.text = "Vidas: " + vidas;
    }
}
