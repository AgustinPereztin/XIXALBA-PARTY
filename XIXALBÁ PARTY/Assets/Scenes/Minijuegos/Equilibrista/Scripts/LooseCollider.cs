using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseCollider : MonoBehaviour
{
    EquilibristaGameplay manager;
    void Start()
    {
        manager = FindFirstObjectByType<EquilibristaGameplay>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.Loose();
    }
}
