using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 30;

    PlayerController playerController;
    PlayerAttackAreaScript playerAttack;
    Rigidbody2D rb;

    private float nextRangedDamage;
    private float nextRangedDamageRate = 0.5f;

    private void Start()
    {
        playerController = GameObject.Find("PlayerContainer").GetComponent<PlayerController>();
        playerAttack = GameObject.Find("Player_Y").GetComponentInChildren<PlayerAttackAreaScript>();
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(playerController.facingDirection * speed, 0), ForceMode2D.Impulse);

        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && Time.time > nextRangedDamage)
        {
            nextRangedDamage = Time.time + nextRangedDamageRate;
            Health enemyHealth = other.gameObject.GetComponent<Health>();
            enemyHealth.AddDamage(playerAttack.playerDamage);
            Destroy(gameObject);
        }
        
    }
}
