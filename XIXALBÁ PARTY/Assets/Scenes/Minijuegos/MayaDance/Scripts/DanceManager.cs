using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DanceManager : MonoBehaviour
{
    public TextMeshProUGUI teclaText;
    public TextMeshProUGUI resultadoText;
    public TextMeshProUGUI contadorTiempo;
    public int timeToWin;
    bool alreadyLost;

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
    private float Erroneas;

    private bool puedeJugar = false;


    void Start()
    {
        StartCoroutine(InicioCuentaAtras());
        
    }

    void Update()
    {
        if (!puedeJugar) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ResultadoIncorrecto();
            GenerarNuevaTecla();
        }

        if (Input.inputString != "")
        {
            char keyPressed = Input.inputString.ToLower()[0];

            if (keyPressed == char.ToLower(teclaActual))
            {
                ResultadoCorrecto();
                GenerarNuevaTecla();
            }
            else if ("qwerasdfw".Contains(keyPressed))
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
        if (alreadyLost)
            return;
        alreadyLost = true;
        StopAllCoroutines();
        resultadoText.color = new Color32(255, 0, 0, 255);
        resultadoText.text = "¡PERDISTE!";
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
        Erroneas++;
        if (Erroneas >= 3) { 
            Perder();
        }
    }
    IEnumerator InicioCuentaAtras()
    {
        for (int i = 3; i > 0; i--)
        {
            contadorTiempo.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        contadorTiempo.text = "¡GO!";
        
        yield return new WaitForSeconds(1);

        puedeJugar = true;
        GenerarNuevaTecla();
        for (int i = timeToWin; i > 0; i--)
        {
            contadorTiempo.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        

        Win();
    }
    void Win()
    {
        StopAllCoroutines();
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }
}
