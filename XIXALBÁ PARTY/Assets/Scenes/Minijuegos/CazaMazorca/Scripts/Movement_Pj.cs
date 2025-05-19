using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // velocidad del personaje

    void Update()
    {
        // Obtiene el input horizontal (A/D o flechas)
        float moveInput = Input.GetAxis("Horizontal");

        // Mueve al personaje en el eje X
        Vector3 move = new Vector3(moveInput, 0f, 0f) * speed * Time.deltaTime;

        transform.position += move;
    }
}
