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

    public GameObject standUpSprite, standDownSprite;
    public BoxCollider2D myCollider;
    private Rigidbody2D rb;
    private bool isGrounded;
    bool alreadyLost, started;
    public AudioSource Roar;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(JaguarSpawners());
        StartCoroutine(TimeToWin());
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

        if (Input.GetKeyDown(KeyCode.S))
        {
            myCollider.size = new Vector2(1, 1);
            myCollider.offset = new Vector2(0, -0.5f);
            standUpSprite.SetActive(false);
            standDownSprite.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            myCollider.size = new Vector2(1, 2);
            myCollider.offset = new Vector2(0, 0);
            standUpSprite.SetActive(true);
            standDownSprite.SetActive(false);
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
                

                // Asegurarse que mire hacia la derecha (scale.x positivo)
                Vector3 scale = jaguarInstance.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                jaguarInstance.transform.localScale = scale;
                yield return new WaitForSeconds(0.3f);
                Roar.Play();
            }
            else
            {
                GameObject jaguarInstance = Instantiate(jaguar, right.position, right.rotation);

                // Asegurarse que mire hacia la izquierda (scale.x negativo)
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
        for(int i = timeToWin; i >= 0; i--)
        {
            contador.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        contador.text = "Ganaste";
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
        contador.text = "Perdiste";
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<JaguarEnemy>() != null)
        {
            Die();
        }
    }
}
