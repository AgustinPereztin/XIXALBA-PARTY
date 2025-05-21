using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerPrincipal : MonoBehaviour
{
    public static GameManagerPrincipal instance;

    public string[] minijuegoScenes;  // todas las scenes
    private List<string> minijuegosPendientes;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InicializarLista();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InicializarLista()
    {
        minijuegosPendientes = new List<string>(minijuegoScenes);
    }

    public void CargarMinijuegoAleatorio()
    {
        if (minijuegosPendientes.Count == 0)
        {
            // Si se terminaron, volver al menú o reiniciar
            SceneManager.LoadScene("MainMenu");
            InicializarLista();
            return;
        }

        int randomIndex = Random.Range(0, minijuegosPendientes.Count);
        string sceneName = minijuegosPendientes[randomIndex];

        // Eliminar ese minijuego de la lista de pendientes
        minijuegosPendientes.RemoveAt(randomIndex);

        // Cargar minijuego
        SceneManager.LoadScene(sceneName);
    }
}
