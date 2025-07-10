using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform objetivo;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - objetivo.position;
    }

    void LateUpdate()
    {
        transform.position = objetivo.position + offset;
    }
}
