using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] mazorcaPrefabs;

    public float spawnInterval = 1.5f;

    public float minX1 = -4f;
    public float maxX1 = 1f;

    public float minX2 = 1f;
    public float maxX2 = 3f;

    public float spawnY = 5f;
    public GameObject sombraPrefab;
    public float sombraY = -3.5f; // altura del suelo donde aparece la sombra

    void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1);
        InvokeRepeating("SpawnMazorca", 0f, spawnInterval);
    }

    void SpawnMazorca()
    {
        int intervaloElegido = Random.Range(0, 2);
        float randomX;

        if (intervaloElegido == 0)
        {
            randomX = Random.Range(minX1, maxX1);
        }
        else
        {
            randomX = Random.Range(minX2, maxX2);
        }

        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        int randomIndex = Random.Range(0, mazorcaPrefabs.Length);
        GameObject mazorcaSeleccionada = mazorcaPrefabs[randomIndex];

        // Instanciar mazorca
        Instantiate(mazorcaSeleccionada, spawnPosition, Quaternion.identity);

        // Instanciar sombra
        Vector3 sombraPosition = new Vector3(randomX, sombraY, 0f);
        Instantiate(sombraPrefab, sombraPosition, Quaternion.identity, transform).GetComponent<DestroyAfterTime>().SetTimer(1f);
    }

    public void DetenerSpawns()
    {
        CancelInvoke("SpawnMazorca");
    }
}