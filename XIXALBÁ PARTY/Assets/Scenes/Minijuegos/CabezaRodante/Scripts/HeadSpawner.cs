using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform player;
    public float spawnInterval = 2f;
    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBall), 1f, spawnInterval);
    }

    void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        RollingHead rolling = ball.GetComponent<RollingHead>();

        if (rolling != null)
        {
            rolling.player = player;
            rolling.speed = Random.Range(minSpeed, maxSpeed);
        }
    }
}
