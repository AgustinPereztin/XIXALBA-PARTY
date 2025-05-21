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

    public GameObject perdisteText;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
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

        perdisteText.gameObject.SetActive(true);
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();

    }

}
