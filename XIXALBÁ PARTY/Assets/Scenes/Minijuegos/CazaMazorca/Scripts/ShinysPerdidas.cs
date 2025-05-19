using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinysPerdidas : MonoBehaviour
{
    
    public Spawner spawner;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto que toca tiene el tag "Player"
        if (other.CompareTag("shiny"))
        {
            GameManager.instance.PerderVida();
           
        }
    }
}
