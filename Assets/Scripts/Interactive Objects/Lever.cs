using Common;
using UnityEngine;

namespace Interactive_Objects
{
    public class Lever : MonoBehaviour
    {
        [Header("Animator Components")]
        [SerializeField] Animator animator;
        [SerializeField] ParameterType parameterType;
        [SerializeField] string parameterName;
        [SerializeField] float floatParameterValue;
        [SerializeField] int intParameterValue;
        [SerializeField] bool boolParameterValue;
        [SerializeField] Sprite lockLever;
        [SerializeField] Sprite unlockLever;

        [Header("Enables and Disables")]
        [SerializeField] GameObject[] enables;
        [SerializeField] GameObject[] disables;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerAttackArea) || other.CompareTag(Tags.Missile) ||
                other.CompareTag(Tags.InteractiveObjects))
            {
                if (animator != null)
                {
                    switch (parameterType)
                    {
                        case ParameterType.Int:
                            animator.SetInteger(parameterName, intParameterValue);
                            break;
                        case ParameterType.Float:
                            animator.SetFloat(parameterName, floatParameterValue);
                            break;
                        case ParameterType.Bool:
                            animator.SetBool(parameterName, boolParameterValue);
                            break;
                        case ParameterType.Trigger:
                            animator.SetTrigger(parameterName);
                            break;
                    }
                }

                LeverObjectsActivator(true, false); // Активирует/деактивирует объекты, которые нужно активировать/деактивировать
                GetComponent<SpriteRenderer>().sprite = unlockLever;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tags.InteractiveObjects))
            {
                LeverObjectsActivator(false, true); // Возвращает объекты в исходное состояние
                GetComponent<SpriteRenderer>().sprite = lockLever;
            }
        }

        private void LeverObjectsActivator(bool enabling, bool disabling) // Функция для активации/деактивации объектов
        {
            if (enables != null)
            {
                foreach (GameObject item in enables)
                {
                    item.SetActive(enabling);
                }
            }

            if (disables != null)
            {
                foreach (var item in disables)
                {
                    item.SetActive(disabling);
                }
            }
        }
    }
}