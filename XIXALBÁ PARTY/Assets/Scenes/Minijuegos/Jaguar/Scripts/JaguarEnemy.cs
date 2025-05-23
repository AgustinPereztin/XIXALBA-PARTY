using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaguarEnemy : MonoBehaviour
{
    public bool startLeft;
    public float speed, jumpForce, timeToJump;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(Random.Range(0,2) == 0)
        {
            StartCoroutine(TimeToJump());
        }
    }

    private void FixedUpdate()
    {
        if (startLeft)
        {
            Vector3 move = Vector2.right * speed * Time.fixedDeltaTime;
            transform.position += move;
        }
        else
        {
            Vector3 move = -Vector2.right * speed * Time.fixedDeltaTime;
            transform.position += move;
        }

        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator TimeToJump()
    {
        yield return new WaitForSeconds(timeToJump);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
