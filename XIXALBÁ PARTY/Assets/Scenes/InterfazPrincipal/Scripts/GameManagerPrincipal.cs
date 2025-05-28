using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

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
        StartCoroutine(TransicionDeLvl());
    }

    IEnumerator TransicionDeLvl()
    {
        if (minijuegosPendientes.Count == 0)
        {
            FindObjectOfType<TransicionCanvas>().EndLvl();
            yield return new WaitForSeconds(0.75f);

            var progress = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

            while (!progress.isDone)
            {
                yield return null;
            }
            InicializarLista();
            Debug.Log("Level loaded");
        }
        else
        {
            int randomIndex = Random.Range(0, minijuegosPendientes.Count);
            string sceneName = minijuegosPendientes[randomIndex];

            // Eliminar ese minijuego de la lista de pendientes
            minijuegosPendientes.RemoveAt(randomIndex);

            // Cargar minijuego
            //SceneManager.LoadScene(sceneName);

            FindObjectOfType<TransicionCanvas>().EndLvl();
            yield return new WaitForSeconds(0.75f);

            var progress = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (!progress.isDone)
            {
                yield return null;
            }

            Debug.Log("Level loaded");
        }
    }
}
