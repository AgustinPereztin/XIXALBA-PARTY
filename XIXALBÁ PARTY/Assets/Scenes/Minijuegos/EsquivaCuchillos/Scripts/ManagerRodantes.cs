using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerRodantes : MonoBehaviour
{
    public float gameTimer;
    public TextMeshProUGUI timerText;
    

    // Update is called once per frame
    void Update()
    {
        gameTimer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(gameTimer).ToString();
        if (gameTimer <= 0f)
        {
            GameWon();
        }
    }
    void GameWon()
    {

        StopAllCoroutines();


        GameManagerPrincipal.instance.SumarVictoria();
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();

    }
}
