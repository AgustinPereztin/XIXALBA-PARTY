using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalScoreManager : MonoBehaviour
{
    public TextMeshProUGUI textoPuntaje;
    public AudioSource victoria;

    void Start()
    {
        victoria.Play();
        int puntaje = GameManagerPrincipal.instance.minijuegosGanados;
        textoPuntaje.text = "PUNTAJE FINAL: "+ puntaje+ "/6";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManagerPrincipal.instance.ReiniciarJuego();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
