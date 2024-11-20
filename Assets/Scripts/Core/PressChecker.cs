using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class PressChecker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool isPressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;
        }
    }
}