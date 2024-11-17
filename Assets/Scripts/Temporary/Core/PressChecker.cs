using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PressChecker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
    [FormerlySerializedAs("isPressed")] public bool _isPressed;
    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }
}
