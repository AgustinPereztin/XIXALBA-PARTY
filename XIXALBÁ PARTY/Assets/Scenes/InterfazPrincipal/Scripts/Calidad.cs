using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calidad : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int calidad;

    void Start()
    {
        calidad = PlayerPrefs.GetInt("numeroDeCalidad", 3);
        dropdown.value = calidad;
        AjustarCalidad();

    }

    public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("numeroDeCalidad", dropdown.value);
        calidad=dropdown.value;
    }
}
