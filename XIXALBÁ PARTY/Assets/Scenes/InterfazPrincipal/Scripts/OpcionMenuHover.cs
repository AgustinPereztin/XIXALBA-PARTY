using UnityEngine;
using UnityEngine.EventSystems;

public class OpcionMenuHover : MonoBehaviour, IPointerEnterHandler
{
    public CalendarioRotation calendarioRotation;

    public void OnPointerEnter(PointerEventData eventData)
    {
        calendarioRotation.MoverOpcionAlCentroDesdeHover(transform);
    }
}
