using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    public float velocidad = 5f;

    void Update()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoX, 0f, movimientoZ);

        transform.Translate(movimiento * velocidad * Time.deltaTime);
    }
}
