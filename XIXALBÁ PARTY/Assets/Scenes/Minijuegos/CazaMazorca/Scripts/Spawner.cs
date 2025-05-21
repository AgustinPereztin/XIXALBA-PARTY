using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] mazorcaPrefabs;  // array de Prefab de la mazorca
    public float spawnInterval = 1.5f; // Tiempo entre spawns
    public float minX = -7f; // L�mite izquierdo
    public float maxX = 7f;  // L�mite derecho
    public float spawnY = 5f; // Altura donde aparecen

    void Start()
    {
        // Llama al m�todo SpawnMazorca cada cierto tiempo
        InvokeRepeating("SpawnMazorca", 0f, spawnInterval);
    }

    void SpawnMazorca()
    {
        // Posici�n aleatoria en X dentro del rango
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Elige aleatoriamente una mazorca del array
        int randomIndex = Random.Range(0, mazorcaPrefabs.Length);
        GameObject mazorcaSeleccionada = mazorcaPrefabs[randomIndex];

        // Instancia la mazorca
        Instantiate(mazorcaSeleccionada, spawnPosition, Quaternion.identity);

    }
    public void DetenerSpawns()
    {
        CancelInvoke("SpawnMazorca");
    }
}
