using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPrincipal : MonoBehaviour
{
    public static GameManagerPrincipal instance;

    public string[] minijuegoScenes;  // Nombres de las scenes de minijuegos

    void Awake()
    {
        // Que no se destruya entre escenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CargarMinijuegoAleatorio()
    {
        int randomIndex = Random.Range(0, minijuegoScenes.Length);
        string sceneName = minijuegoScenes[randomIndex];
        SceneManager.LoadScene(sceneName);
    }
}
