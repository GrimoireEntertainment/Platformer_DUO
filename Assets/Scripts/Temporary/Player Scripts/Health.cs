using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void AddDamage(float damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) Destroy(gameObject);
    }
}
