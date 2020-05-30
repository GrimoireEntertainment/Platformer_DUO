using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;

    public void AddDamage(float damage)
    {
        
        currentHealth -= damage;
        if(transform.tag != "Player" && currentHealth <= 0){
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }  
    }
}
