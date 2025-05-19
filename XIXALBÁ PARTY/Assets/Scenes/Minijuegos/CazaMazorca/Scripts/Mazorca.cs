using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mazorca : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto que toca tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruye esta mazorca
        }
    }
}
