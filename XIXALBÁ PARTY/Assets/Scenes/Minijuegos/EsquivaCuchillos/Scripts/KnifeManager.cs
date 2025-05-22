using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // <--- ¡IMPORTANTE!

public class KnifeManager : MonoBehaviour
{
    public GameObject knifePrefab;
    public Transform[] spawners;
    public float spawnInterval = 0.3f;
    public float gameDuration = 60f;

    private float spawnTimer;
    private float gameTimer;

    public TextMeshProUGUI timerText; // <--- CAMBIADO de Text a TextMeshProUGUI
    public GameObject winText;
    public GameObject loseText;

    private bool gameOver = false;

    void Start()
    {
        gameTimer = gameDuration;
        winText.SetActive(false);
        loseText.SetActive(false);
    }

    void Update()
    {
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
        }
    }

    public void GameOver()
    {
        gameOver = true;
        loseText.SetActive(true);
        Invoke("RestartGame", 3f);
    }

    void GameWon()
    {
        gameOver = true;
        winText.SetActive(true);
        Invoke("RestartGame", 3f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
