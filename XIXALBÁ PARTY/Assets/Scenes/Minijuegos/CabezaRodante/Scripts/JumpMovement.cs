using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerRodantesYJump : MonoBehaviour
{
    // Timer y UI
    public float gameTimer;
    public TextMeshProUGUI timerText;

    // Salto y movimiento
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private bool isGrounded;

    // Sonidos
    public AudioSource music;
    public AudioSource JUMP;
    public AudioSource punch;

    // Animación
    public Animator PJanimacion;

    // Control de estados
    private bool alreadyLost, started, juegoTerminado;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        music.Play();
        StartCoroutine(InitialDelay());
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(0.75f);
        started = true;
    }

    void Update()
    {
        if (!started || juegoTerminado)
            return;

        // Actualizar timer
        gameTimer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(gameTimer).ToString();

        if (gameTimer <= 0f)
        {
            GameWon();
        }

        // Control de salto
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            JUMP.Play();
            PJanimacion.SetBool("salto", true);
        }

        if (isGrounded && rb.velocity.y <= 0)
        {
            PJanimacion.SetBool("salto", false);
            PJanimacion.SetBool("caida", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Head"))
        {
            punch.Play();
            Die();
        }
    }

    void Die()
    {
        if (alreadyLost)
            return;

        alreadyLost = true;
        juegoTerminado = true;

        PJanimacion.SetBool("muerte", true);
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }

    void GameWon()
    {
        juegoTerminado = true;
        StopAllCoroutines();
        GameManagerPrincipal.instance.SumarVictoria();
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }
}