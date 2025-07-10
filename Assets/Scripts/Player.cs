using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _gravity = -9.81f;
    public float velocidad = 30f;
    [SerializeField] private float gravityMultiplier = 3.0f;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }


    void Update()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoX, 0f, movimientoZ);

        transform.Translate(movimiento * velocidad * Time.deltaTime);
    }
}
