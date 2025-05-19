using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingHead : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = (player.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Destroy(this.gameObject);
    }
}
