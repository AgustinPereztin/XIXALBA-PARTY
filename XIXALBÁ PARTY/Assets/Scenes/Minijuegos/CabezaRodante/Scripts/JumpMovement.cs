using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpMovement : MonoBehaviour
{
    public float jumpForce = 10f; //fuerza de salto (creo que 10 está bien)
    public Transform groundCheck; 
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround; //todo esto pa chequear que esté en el piso

    private Rigidbody2D rb;
    private bool isGrounded;

    //La pantalla de perdiste 

    public AudioSource JUMP;
    public AudioSource punch;
    public Animator PJanimacion;


    bool alreadyLost, started;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(InitialDelay());
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(0.75f);
        started = true;
    }

    void Update()
    {
        if (!started)
            return;
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
        PJanimacion.SetBool("muerte", true);
        //perdisteText.gameObject.SetActive(true);
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();

    }

}
