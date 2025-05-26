using UnityEngine;
using UnityEngine.EventSystems;

public class BotonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 escalaNormal;
    public Vector3 escalaHover = new Vector3(2f, 2f, 1f);
    public float velocidad = 5f;

    private Vector3 escalaObjetivo;

    void Start()
    {
        escalaNormal = transform.localScale;
        escalaObjetivo = escalaNormal;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, escalaObjetivo, Time.deltaTime * velocidad);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        escalaObjetivo = escalaHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        escalaObjetivo = escalaNormal;
    }
}
