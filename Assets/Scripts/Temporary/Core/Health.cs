using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    [FormerlySerializedAs("currentHealth")] public float _currentHealth;

    public void AddDamage(float damage)
    {
        _currentHealth -= damage;
        if((!transform.CompareTag("Player") && !transform.CompareTag("Flying Dragon")) && _currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
