using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyWeaponScript : MonoBehaviour
{
    [SerializeField] float amountOfDamage;
    [SerializeField] float speed;
    [SerializeField] float destroyAfterSeconds;
    private Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRB.velocity = new Vector2(-speed, myRB.velocity.y);
        Destroy(gameObject, destroyAfterSeconds);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.AddDamage(amountOfDamage);
        }
    }
}
