using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotationMazorca : MonoBehaviour
{
    public float minRotationSpeed = -200f; // velocidad m�nima
    public float maxRotationSpeed = 200f;  // velocidad m�xima
    private float rotationSpeed;

    void Start()
    {
        // Asignar velocidad de rotaci�n aleatoria al iniciar
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {
        // Rota suavemente sobre Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
