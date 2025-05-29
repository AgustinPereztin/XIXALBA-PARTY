using System.Collections;
using TMPro;
using UnityEngine;

public class JumpMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private bool isGrounded;

    public TextMeshProUGUI contador;
    public HeadSpawner headSpawner;
    public float spawnInterval = 2f;

    bool started;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CuentaRegresiva());
    }

    void Update()
    {
        if (!started)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public IEnumerator CuentaRegresiva()
    {
        // Cuenta regresiva inicial
        for (int i = 3; i > 0; i--)
        {
            contador.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        contador.text = "¡GO!";
        started = true;

        // Iniciar spawneo repetido
        InvokeRepeating(nameof(SpawnHead), 0f, spawnInterval);

        // Cuenta regresiva de juego
        for (int i = 10; i > -1; i--)
        {
            contador.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        // Termina el juego
        started = false;
        CancelInvoke(nameof(SpawnHead));

        Win();
    }

    void SpawnHead()
    {
        headSpawner.SpawnBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Head"))
        {
            Die();
        }
    }

    void Die()
    {
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }

    public void Win()
    {
        contador.text = "¡Ganaste!";
        GameManagerPrincipal.instance.SumarVictoria();
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }
}