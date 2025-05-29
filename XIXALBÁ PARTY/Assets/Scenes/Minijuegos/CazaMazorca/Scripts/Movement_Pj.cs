using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovementMazorca : MonoBehaviour
{
    
    public float speed = 5f; // velocidad del personaje
    internal bool puedeMoverse = true;
    private Animator animator;
    bool started;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.6f);
        started = true;
    }
    void Update()
    {
        if (!puedeMoverse || !started) return;

        float moveInput = Input.GetAxis("Horizontal");

        // Mover al personaje
        Vector3 move = new Vector3(moveInput, 0f, 0f) * speed * Time.deltaTime;
        transform.position += move;

        float clampedX = Mathf.Clamp(transform.position.x, -4f, 4f); // Cambiá esos números según tu escenario
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Controlar Animator
        if (moveInput > 0)
        {
            animator.SetBool("right", true);
            animator.SetBool("left", false);
        }
        else if (moveInput < 0)
        {
            animator.SetBool("right", false);
            animator.SetBool("left", true);
        }
        else
        {
            // Si no se mueve, desactivo ambos
            animator.SetBool("right", false);
            animator.SetBool("left", false);
        }
    }
    public void RecibirDaño()
    {
        animator.SetTrigger("damage");
    }
}
