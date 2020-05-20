using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Animator Components")]
    [SerializeField] Animator animator;
    [SerializeField] ParametrType parameterType;
    [SerializeField] string parameterName;
    [SerializeField] float floatParameterValue;
    [SerializeField] int intParameterValue;
    [SerializeField] bool boolParameterValue;
    [Header("Enables and Disables")]
    [SerializeField] GameObject[] enables;
    [SerializeField] GameObject[] disables;


    public enum ParametrType
    {
        Int,
        Float,
        Bool,
        Trigger
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttackArea" || other.tag == "Missile" || other.tag == "InteractiveObjects")
        {
            if(animator != null)
            {
                switch (parameterType)
                {
                    case ParametrType.Int:
                        animator.SetInteger(parameterName, intParameterValue);
                        break;
                    case ParametrType.Float:
                        animator.SetFloat(parameterName, floatParameterValue);
                        break;
                    case ParametrType.Bool:
                        animator.SetBool(parameterName, boolParameterValue);
                        break;
                    case ParametrType.Trigger:
                        animator.SetTrigger(parameterName);
                        break;
                }
            }
            
            LeverObjectsActivator(true, false); // Активирует/деактивирует объекты, которые нужно активировать/деактивировать
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "InteractiveObjects")
        {
            LeverObjectsActivator(false, true); // Возвращает объекты в исходное состояние
        }
    }

    private void LeverObjectsActivator(bool enabling, bool disabling) // Функция для активации/деактивации объектов
    {
        if(enables != null)
            {
                foreach (GameObject item in enables)
                {
                    item.SetActive(enabling);
                }
            }

            if(disables != null)
            {
                foreach (var item in disables)
                {
                    item.SetActive(disabling);
                }
            }
    }
}
