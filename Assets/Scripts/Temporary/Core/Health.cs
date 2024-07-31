using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;

    public void AddDamage(float damage)
    {
        currentHealth -= damage;
        if((!transform.CompareTag("Player") && !transform.CompareTag("Flying Dragon")) && currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
