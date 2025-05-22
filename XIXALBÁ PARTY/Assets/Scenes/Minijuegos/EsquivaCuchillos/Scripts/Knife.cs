using UnityEngine;

public class Knife : MonoBehaviour
{
    void Update()
    {
        // Si cae muy abajo, lo destruimos para que no consuma memoria
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Llama al GameManager para terminar el juego
            FindObjectOfType<KnifeManager>().GameOver();

            // Destruye este cuchillo
            Destroy(gameObject);
        }
    }
}
