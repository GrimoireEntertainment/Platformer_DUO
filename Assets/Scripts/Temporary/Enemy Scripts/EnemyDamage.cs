using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] float enemyDamage;
    [SerializeField] float damageRate;
    float nextDamage = 0.0f;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && Time.time > nextDamage) {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.AddDamage(enemyDamage);
            nextDamage = Time.time + damageRate;
        }
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
