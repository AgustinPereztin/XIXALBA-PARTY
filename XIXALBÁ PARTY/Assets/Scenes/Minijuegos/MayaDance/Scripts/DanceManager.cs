using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DanceManager : MonoBehaviour
{
    public TextMeshProUGUI teclaText;
    public TextMeshProUGUI resultadoText;
    
    public RectTransform BgTeclaRect;
    public string[] posiblesMensajes = {
        "¡Muy bien!",
        "¡Que Bien Bailas!",
        "¡Guauu!",
        "¡Exelente!"
    };
    public string[] posiblesMensajesIncorrectos = {
        "¡Horrible!",
        "¡Que MAL Bailas!",
        "¡Das Asco!",
        "¡Rindete!"
    };

    public float tiempoLimite = 2f;

    private char teclaActual;
    private float timer;

    public RectTransform textoRect;
    public float minX = -400f;
    public float maxX = 400f;
    public float minY = -200f;
    public float maxY = 200f;

    void Start()
    {
        GenerarNuevaTecla();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Si pasa el tiempo, pierde
        if (timer <= 0)
        {
            ResultadoIncorrecto();
            GenerarNuevaTecla();
            //Perder();
        }

        // Si toca la tecla correcta
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(teclaActual.ToString().ToLower()))
            {
                ResultadoCorrecto();
                GenerarNuevaTecla();
            }
            else
            {
                ResultadoIncorrecto();
                GenerarNuevaTecla();
            }
        }
    }

    void GenerarNuevaTecla()
    {
        string teclasPosibles = "QWERASDFW";
        int randomIndex = Random.Range(0, teclasPosibles.Length);
        teclaActual = teclasPosibles[randomIndex];

        teclaText.text = teclaActual.ToString();
        timer = tiempoLimite;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        
        BgTeclaRect.anchoredPosition = new Vector2(randomX, randomY);
    }

    void Perder()
    {
        
        
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }

    void ResultadoCorrecto() {

        int randomIndex = Random.Range(0, posiblesMensajes.Length);
        resultadoText.text = posiblesMensajes[randomIndex];
        resultadoText.color = Color.green;
    }
    void ResultadoIncorrecto()
    {

        int randomIndex = Random.Range(0, posiblesMensajesIncorrectos.Length);
        resultadoText.text = posiblesMensajesIncorrectos[randomIndex];
        resultadoText.color = new Color32(255, 0, 0, 255);
    }
}
