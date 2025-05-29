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
    public PlayerMovementMazorca player;


    public bool alreadyLost;

    public Spawner spawner;

    public float Mazorcas_A_Agarrar = 10;
    private float MazorcasAgarradas;
    public AudioSource damageSound;
    public AudioSource mazorcaSound;

    void Awake()
    {
        instance = this;
    }

    public void PerderVida()
    {
        vidas--;
        vidasText.text = "Vidas: " + vidas;
        player.RecibirDaño(); // << activa animación de daño
        damageSound.Play();

        if (vidas <= 0)
        {
            if (alreadyLost)
                return;
            alreadyLost = true;
            spawner.DetenerSpawns();
            vidasText.text = "";
            resultado.text = "¡Perdiste!";
            GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
        }
    }
    public void Ganar()
    {
        if (alreadyLost)
            return;
        MazorcasAgarradas ++;

        mazorcaSound.Play();

        if (MazorcasAgarradas == Mazorcas_A_Agarrar)
        {
            spawner.DetenerSpawns();
            vidasText.text = "";
            resultado.text = "¡Ganaste!";
            GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
        }
    }
    

}
