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
            Health enemyHealth = other.gameObject.GetComponent<Health>();
            enemyHealth.AddDamage(playerDamage);
            nextDamage = Time.time + damageRate;
        }
        
    }
}