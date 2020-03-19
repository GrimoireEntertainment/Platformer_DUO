using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAreaScript : MonoBehaviour
{
    [SerializeField] PressChecker swordAttackButton;
    [SerializeField] float playerDamage;
    [SerializeField] float damageRate;
    float nextDamage = 0.0f;

    private void OnTriggerStay2D(Collider2D other) {
        
        if((Input.GetKey(KeyCode.Mouse0) || swordAttackButton.isPressed) && other.tag == "Enemy" && Time.time > nextDamage) {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.AddDamage(playerDamage);
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
