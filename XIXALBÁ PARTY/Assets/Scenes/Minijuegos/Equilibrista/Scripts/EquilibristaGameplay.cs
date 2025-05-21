using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EquilibristaGameplay : MonoBehaviour
{
    public GameObject mano;
    public Transform totem;
    public Rigidbody2D totemRb;
    public float speed = 5f, rotation = 20f; // velocidad de la mano
    public TextMeshProUGUI contador;
    bool started; //Indica cuando empieza el juego

    void Start()
    {
        StartCoroutine(CuentaRegresiva());
        totemRb = totem.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Si no empiezo el juego no se ejecuta la siguiente parte del codigo
        if (!started)
            return;

        // Obtiene el input horizontal (A/D o flechas)
        float moveInput = Input.GetAxis("Horizontal");

        // Mueve al personaje en el eje X
        Vector3 move = new Vector3(moveInput, 0f, 0f) * speed * Time.fixedDeltaTime;

        mano.transform.position += move;

        mano.transform.position = new Vector2(Mathf.Clamp(mano.transform.position.x, - 6.85f, 6.85f), mano.transform.position.y);


        //Mecanica del Balanceo
        totemRb.AddTorque(moveInput * rotation * Time.fixedDeltaTime);
    }
    
    public IEnumerator CuentaRegresiva()
    {
        //Cuenta regresiva del 3 al 0
        for(int i = 3; i > 0; i--)
        {
            contador.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        contador.text = "¡GO!";
        //Empieza el juego
        started = true;

        totemRb.AddTorque(Random.Range(-50,50));

        //Cuenta regresiva del para terminar el juego
        for (int i = 10; i > -1; i--)
        {
            yield return new WaitForSeconds(1);
            contador.text = i.ToString();
        }

        started = false;
        totemRb.isKinematic = true;
        Win();
    }

    public void Loose()
    {
        if (!started)
            return;
        StopAllCoroutines();
        contador.text = "¡Perdiste!";
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }

    public void Win()
    {
        contador.text = "¡Ganaste!";
        GameManagerPrincipal.instance.CargarMinijuegoAleatorio();
    }
}
