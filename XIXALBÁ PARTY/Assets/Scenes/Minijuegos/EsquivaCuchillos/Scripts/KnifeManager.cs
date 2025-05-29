using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections; // <--- ¡IMPORTANTE!

public class KnifeManager : MonoBehaviour
{
    public PlayerMovementCuchillos player; //referencio al player para parar sus movimiento
    public GameObject knifePrefab;
    public Transform[] spawners;
    public float spawnInterval = 0.3f;
    public int gameDuration;

    private float spawnTimer;
    private float gameTimer;

    public TextMeshProUGUI timerText; // <--- CAMBIADO de Text a TextMeshProUGUI
    public GameObject winText;
    public GameObject loseText;
    public AudioSource flechas;

    private bool puedeJugar = false;
    private bool gameOver = false;

    bool alreadyLost;

    void Start()
    {
        StartCoroutine(InicioCuentaAtras());
        gameTimer = gameDuration;
        winText.SetActive(false);
        loseText.SetActive(false);
    }

    void Update()
    {
        if (!puedeJugar) return;
        if (gameOver) return;

        // Cronómetro
        gameTimer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(gameTimer).ToString();

        if (gameTimer <= 0f)
        {
            GameWon();
        }

        // Spawning de cuchillos
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            int index = Random.Range(0, spawners.Length);
            Instantiate(knifePrefab, spawners[index].position, Quaternion.identity);
            spawnTimer = 0f;
            flechas.Play();
        }
        
    }
    IEnumerator InicioCuentaAtras()
    {
        for (int i = 3; i > 0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        timerText.text = "¡GO!";

        yield return new WaitForSeconds(1);

        puedeJugar = true;
        
        for (int i = gameDuration; i > 0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        GameWon();
    }

    public void GameOver()
    {
        if (alreadyLost)
            return;
        alreadyLost = true;
        player.puedeMoverse = false;
        StopAllCoroutines();
        gameOver = true;
        //loseText.SetActive(true);
        //Invoke("RestartGame", 3f);
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }

    void GameWon()
    {
        player.puedeMoverse = false;
        StopAllCoroutines();
        gameOver = true;
        //winText.SetActive(true);
        //Invoke("RestartGame", 3f);
        GameManagerPrincipal.instance.SumarVictoria();
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();

    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
