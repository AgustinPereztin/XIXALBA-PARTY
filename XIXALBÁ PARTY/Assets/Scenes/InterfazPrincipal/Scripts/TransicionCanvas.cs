using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicionCanvas : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void EndLvl()
    {
        anim.SetTrigger("LvlEnd");
    }
}
