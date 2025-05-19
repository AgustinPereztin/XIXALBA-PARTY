using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Mazorca : MonoBehaviour
{
    public Spawner spawner;
    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI resultado;

    

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto que toca tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruye esta mazorca
            GameManager.instance.Ganar();
        }
        
    }
}
