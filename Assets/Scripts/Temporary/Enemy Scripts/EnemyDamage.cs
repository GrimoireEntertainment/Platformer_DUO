using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] float enemyDamage;
    [SerializeField] float damageRate;
    float nextDamage = 0.0f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!CompareTag("Player")) return;

        if(Time.time > nextDamage) {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.AddDamage(enemyDamage);
            nextDamage = Time.time + damageRate;
        }
    }
    
}
