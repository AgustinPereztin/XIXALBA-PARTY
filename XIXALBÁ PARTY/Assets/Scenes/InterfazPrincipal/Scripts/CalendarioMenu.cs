using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarioRotation : MonoBehaviour
{
    public float rotationSpeed = 200f;
    private bool girar = true;

    void Update()
    {
        if (girar)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

}

