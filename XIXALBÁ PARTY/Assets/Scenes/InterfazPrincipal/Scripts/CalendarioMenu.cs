using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarioRotation : MonoBehaviour
{
    public Transform calendario;
    public Transform objetivo;

    public float speed = 200f;

    private float targetRotation;
    private bool rotating = false;

    // Nuevo: método para mover desde hover
    public void MoverOpcionAlCentroDesdeHover(Transform opcion)
    {
        Vector2 dirOpcion = opcion.position - calendario.position;
        Vector2 dirObjetivo = objetivo.position - calendario.position;

        float angulo = Vector2.SignedAngle(dirOpcion, dirObjetivo);

        // Si ya está rotando, acumulá sobre la posición actual
        targetRotation = calendario.eulerAngles.z + angulo;
        rotating = true;
    }

    public void MoverOpcionAlCentro(Transform opcion)
    {
        Vector2 dirOpcion = opcion.position - calendario.position;
        Vector2 dirObjetivo = objetivo.position - calendario.position;

        float angulo = Vector2.SignedAngle(dirOpcion, dirObjetivo);

        targetRotation = calendario.eulerAngles.z + angulo;
        rotating = true;
    }

    void Update()
    {
        if (rotating)
        {
            float currentZ = calendario.eulerAngles.z;
            float newZ = Mathf.MoveTowardsAngle(currentZ, targetRotation, speed * Time.deltaTime);
            calendario.eulerAngles = new Vector3(0, 0, newZ);

            if (Mathf.Approximately(newZ, targetRotation))
            {
                rotating = false;
            }
        }
    }
}

