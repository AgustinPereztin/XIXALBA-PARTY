using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI resultado;
    public int vidas = 3;

    public Spawner spawner;

    void Awake()
    {
        instance = this;
    }

    public void PerderVida()
    {
        vidas--;
        vidasText.text = "Vidas: " + vidas;

        if (vidas <= 0)
        {
            spawner.DetenerSpawns();
            vidasText.text = "";
            resultado.text = "¡Perdiste!";


        }
    }
}
