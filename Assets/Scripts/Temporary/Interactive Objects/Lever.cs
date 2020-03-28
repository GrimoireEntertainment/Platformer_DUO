using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Animator Components")]
    [SerializeField] Animator animator;
    [SerializeField] string parametrName;
    [SerializeField] float floatParametrValue;
    [SerializeField] int intParametrValue;
    [SerializeField] bool boolParametrValue;
    [SerializeField] ParametrType parametrType;
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
        if (other.tag == "PlayerAttackArea")
        {
            print("ok");
            if(animator != null)
            {
                switch (parametrType)
                {
                    case ParametrType.Int:
                        animator.SetInteger(parametrName, intParametrValue);
                        break;
                    case ParametrType.Float:
                        animator.SetFloat(parametrName, floatParametrValue);
                        break;
                    case ParametrType.Bool:
                        animator.SetBool(parametrName, boolParametrValue);
                        break;
                    case ParametrType.Trigger:
                        animator.SetTrigger(parametrName);
                        break;
                }
            }
                
            if(enables != null)
            {
                foreach (GameObject item in enables)
                {
                    item.SetActive(true);
                }
            }

            if(disables != null)
            {
                foreach (var item in disables)
                {
                    item.SetActive(false);
                }
            }

        }
    }
}
