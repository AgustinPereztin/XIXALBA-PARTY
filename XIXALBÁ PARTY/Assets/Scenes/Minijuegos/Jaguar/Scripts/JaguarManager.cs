using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JaguarManager : MonoBehaviour
{
    public TextMeshProUGUI contador;
    public GameObject jaguar;
    public Transform left, right;
    public int timeToWin;
    public float minTime, maxTime;
    public bool lost;

    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public BoxCollider2D myCollider;
    private Rigidbody2D rb;
    private bool isGrounded;
    bool alreadyLost, started;
    public AudioSource Roar;
    public AudioSource jump;
    public AudioSource punch;
    public AudioSource music;
    public Animator PJanimacion; // Animator asignado
    public float normalSizeY = 4f;
    public float crouchSizeY = 1f;

    public float normalOffsetY = 0f;
    public float crouchOffsetY = -0.5f;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(JaguarSpawners());
        StartCoroutine(TimeToWin());
        music.Play();
    }

    void Update()
    {
        if (!started)
            return;

        // Chequear si está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            PJanimacion.SetBool("salto", true);
            jump.Play();
        }

        // Animación de caída
        PJanimacion.SetBool("caida", rb.velocity.y < -0.1f);

        // Cuando está en el suelo, desactiva salto y caída
        if (isGrounded && rb.velocity.y <= 0)
        {
            PJanimacion.SetBool("salto", false);
            PJanimacion.SetBool("caida", false);
        }

        // Agacharse
        if (Input.GetKeyDown(KeyCode.S))
        {
            myCollider.size = new Vector2(myCollider.size.x, crouchSizeY);
            myCollider.offset = new Vector2(myCollider.offset.x, crouchOffsetY);
            PJanimacion.SetBool("agachar", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            myCollider.size = new Vector2(myCollider.size.x, normalSizeY);
            myCollider.offset = new Vector2(myCollider.offset.x, normalOffsetY);
            PJanimacion.SetBool("agachar", false);
        }
    }

    IEnumerator JaguarSpawners()
    {
        yield return new WaitForSeconds(0.75f);
        started = true;
        yield return new WaitForSeconds(1);

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            if (Random.Range(0, 2) == 0)
            {
                GameObject jaguarInstance = Instantiate(jaguar, left.position, left.rotation);
                jaguarInstance.GetComponent<JaguarEnemy>().startLeft = true;

                Vector3 scale = jaguarInstance.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                jaguarInstance.transform.localScale = scale;

                yield return new WaitForSeconds(0.3f);
                Roar.Play();
            }
            else
            {
                GameObject jaguarInstance = Instantiate(jaguar, right.position, right.rotation);

                Vector3 scale = jaguarInstance.transform.localScale;
                scale.x = -Mathf.Abs(scale.x);
                jaguarInstance.transform.localScale = scale;

                yield return new WaitForSeconds(0.3f);
                Roar.Play();
            }
        }
    }

    IEnumerator TimeToWin()
    {
        for (int i = timeToWin; i >= 0; i--)
        {
            contador.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        contador.text = "0";
        StopAllCoroutines();
        Win();
    }

    public void Win()
    {
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }

    public void Die()
    {
        if (alreadyLost)
            return;

        alreadyLost = true;
        StopAllCoroutines();
        lost = true;
        
        PJanimacion.SetBool("salto", false);
        PJanimacion.SetBool("caida", false);
        PJanimacion.SetBool("agachar", false);
        PJanimacion.SetBool("muerte", true); // 👉 activar animación de muerte
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<JaguarEnemy>() != null)
        {
            punch.Play();
            Die();
        }
    }
}