using UnityEngine;

public class FlyingEnemyMissileScript : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _missileDamage;
    [SerializeField] float _destroyAfterSeconds;
    private Transform player;
    private Rigidbody2D RB;
    private Vector2 moveDirection;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        moveDirection = (player.position - transform.position).normalized * _speed;
        RB.AddForce(moveDirection);
        Destroy(gameObject, _destroyAfterSeconds);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.AddDamage(_missileDamage);
            Destroy(gameObject);
        }
    }
}
