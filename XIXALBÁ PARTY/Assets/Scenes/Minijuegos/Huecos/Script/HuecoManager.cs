using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HuecoManager : MonoBehaviour
{
    public Transform[] holes;
    public GameObject friendPrefab;
    public GameObject enemyPrefab;
    public GameObject losePanel;
    public GameObject winPanel;
    public TMP_Text scoreHueco;
    public TMP_Text introHuecos;

    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 1.5f;
    public int pointsToWin = 10;
    public float introDuration = 5f;

    private int score = 0;
    private bool gameActive = false;
    private bool[] holeOccupied;

    bool alreadyLost;
    private void Start()
    {
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        introHuecos.gameObject.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        score = 0;
        UpdateScoreText();

        holeOccupied = new bool[holes.Length]; 

        yield return new WaitForSeconds(introDuration);

        introHuecos.gameObject.SetActive(false);
        gameActive = true;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (gameActive)
        {
            SpawnCharacter();
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnCharacter()
    {
        
        List<int> freeIndices = new List<int>();
        for (int i = 0; i < holes.Length; i++)
        {
            if (!holeOccupied[i])
                freeIndices.Add(i);
        }

        if (freeIndices.Count == 0) return;

        int randomIndex = freeIndices[Random.Range(0, freeIndices.Count)];
        Transform hole = holes[randomIndex];

        GameObject prefabToSpawn = Random.value < 0.3f ? friendPrefab : enemyPrefab;
        GameObject obj = Instantiate(prefabToSpawn, hole.position, Quaternion.identity);

        holeOccupied[randomIndex] = true;

        FriendOrEnemy foe = obj.GetComponent<FriendOrEnemy>();
        if (foe != null)
        {
            foe.manager = this;
        }

       
        StartCoroutine(FreeHoleAfterDelay(randomIndex, 1.2f));

        Destroy(obj, 1.2f);
    }

    IEnumerator FreeHoleAfterDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        holeOccupied[index] = false;
    }

    public void OnEnemyClicked()
    {
        if (!gameActive) return;

        score++;
        UpdateScoreText();

        if (score >= pointsToWin)
        {
            WinGame();
        }
    }

    public void OnFriendClicked()
    {
        if (!gameActive) return;

        losePanel.SetActive(true);
        gameActive = false;
        StopAllCoroutines();
        Invoke("RestartGame", 3f);
    }

    void WinGame()
    {
        gameActive = false;
        winPanel.SetActive(true);
        StopAllCoroutines();
        Invoke("RestartGame", 3f);
    }

    void RestartGame()
    {
        if (alreadyLost)
            return;
        alreadyLost = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    void UpdateScoreText()
    {
        if (scoreHueco != null)
            scoreHueco.text = "Score: " + score;
    }
}
