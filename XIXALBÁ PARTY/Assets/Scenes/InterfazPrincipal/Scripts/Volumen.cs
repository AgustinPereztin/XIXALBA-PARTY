using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;
    public AudioMixer audioMixer;  // Referencia al AudioMixer

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        CambiarVolumen(slider.value);
        RevisarSiEstoyMute();
    }

    public void CambiarVolumen(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);

        // Cambiar volumen del mixer (logarítmico porque Unity trabaja así)
        audioMixer.SetFloat("VolumenGeneral", Mathf.Log10(valor) * 20);

        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute()
    {
        //imagenMute.enabled = (sliderValue <= 0.001f);
    }
}
