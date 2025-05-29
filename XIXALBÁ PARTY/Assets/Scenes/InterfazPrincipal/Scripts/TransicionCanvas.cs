using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicionCanvas : MonoBehaviour
{
    Animator anim;
    public AudioSource puertas;
    private void Start()
    {
        anim = GetComponent<Animator>();
        puertas.Play();
    }

    public void EndLvl()
    {
        
        anim.SetTrigger("LvlEnd");
        
    }
}
