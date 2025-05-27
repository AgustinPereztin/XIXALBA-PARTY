using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void SalirDelJuego()
    {
        // Si estás en el editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estás en un build real
        Application.Quit();
#endif
    }
}