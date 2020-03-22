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
        //Конфликт - Input.GetKey(KeyCode.Mouse0) и swordAttackButton.isPressed
        // насколько я понял на телефоне простое нажатие по экрану воспринимается как Input.GetKey(KeyCode.Mouse0)
        if((Input.GetKey(KeyCode.K) || swordAttackButton.isPressed) && other.tag == "Enemy" && Time.time > nextDamage)
        {
            print("Attacking");
            Health enemyHealth = other.gameObject.GetComponent<Health>();
            enemyHealth.AddDamage(playerDamage);
            nextDamage = Time.time + damageRate;
        }
        
    }
}