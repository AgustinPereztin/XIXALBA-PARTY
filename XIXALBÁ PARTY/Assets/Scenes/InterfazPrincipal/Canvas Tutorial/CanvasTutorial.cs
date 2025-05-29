using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTutorial : MonoBehaviour
{
    public int lvl;
    public GameObject tutorial;
    void Start()
    {
        tutorial.SetActive(false);
        StartCoroutine(ShowTutorial());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            tutorial.SetActive(false);
        }
    }

    IEnumerator ShowTutorial()
    {
        yield return new WaitForSeconds(0.5f);
        tutorial.SetActive(true);
        Time.timeScale = 0;
    }
}
