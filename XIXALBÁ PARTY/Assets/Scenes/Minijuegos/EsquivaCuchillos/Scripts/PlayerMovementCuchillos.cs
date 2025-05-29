using UnityEngine;

public class PlayerMovementCuchillos : MonoBehaviour
{
    public float speed = 5f;

    private float xLimit;

    public bool puedeMoverse = true;
    public Animator PJanimacion;

    void Start()
    {
        // Calcular el ancho visible de la cámara
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // Restamos un pequeño valor como margen para que no se vea cortado
        xLimit = halfWidth - 0.5f;
    }

    void Update()
    {
        if (!puedeMoverse) return;

        float moveX = Input.GetAxisRaw("Horizontal");

        // Movimiento
        Vector3 position = transform.position;
        position.x += moveX * speed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, -xLimit, xLimit);
        transform.position = position;

        // Animación
        PJanimacion.SetFloat("moveX", moveX);
    }
    }
