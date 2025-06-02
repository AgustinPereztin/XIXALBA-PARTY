using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameManagerPrincipal : MonoBehaviour
{
    public static GameManagerPrincipal instance;

    public string[] minijuegoScenes;  // todas las scenes de minijuegos
    private List<string> minijuegosPendientes;
    public int minijuegosGanados = 0;

    public AudioSource puertas;
    

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
    public void InicializarLista()
    {
        minijuegosPendientes = new List<string>(minijuegoScenes);
    }

    public void CargarMinijuegoAleatorio()
    {
        StartCoroutine(TransicionDeLvl());
    }

   

    

    IEnumerator TransicionDeLvl()
    {
        if (minijuegosPendientes.Count == 0)
        {
            // No quedan minijuegos, ir a puntaje final
            SceneManager.LoadScene("PuntajeFinal");
            yield break;
        }
        else
        {
            int randomIndex = Random.Range(0, minijuegosPendientes.Count);
            string sceneName = minijuegosPendientes[randomIndex];

            // Sacarlo de la lista
            minijuegosPendientes.RemoveAt(randomIndex);

            // Transición de nivel
            FindObjectOfType<TransicionCanvas>().EndLvl();
            yield return new WaitForSeconds(0.75f);

            // Cargar minijuego
            var progress = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!progress.isDone)
            {
                yield return null;
            }

            Debug.Log("Level loaded");
        }
    }

    public void SumarVictoria()
    {
        minijuegosGanados++;
    }
    public void ReiniciarJuego()
    {
        minijuegosGanados = 0;
        InicializarLista();
    }
}