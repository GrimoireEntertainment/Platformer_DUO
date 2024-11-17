using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDamage : MonoBehaviour
{
    [FormerlySerializedAs("enemyDamage")] [SerializeField] float _enemyDamage;
    [SerializeField] float damageRate;
    float nextDamage = 0.0f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!CompareTag("Player")) return;

        if(Time.time > nextDamage) {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.AddDamage(_enemyDamage);
            nextDamage = Time.time + damageRate;
        }
    }
    
}
