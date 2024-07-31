using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _shootRate;
    [SerializeField] float _distanceFromPlayer;
    [SerializeField] GameObject missile;
    [SerializeField] Transform missileStartPoint;
    private Transform player;
    private Animator dragonAnim;
    private Health dragonHealth;
    private float _shootTime = 0.0f;
    private bool _isPlayerDetected = false;
    private bool _isPlayerInZone = false;
    private bool _facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        dragonAnim = GetComponent<Animator>();
        dragonHealth = GetComponentInChildren<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPlayerDetected && Vector2.Distance(transform.position, player.position) > _distanceFromPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, _speed * Time.deltaTime);
        }


        if(_isPlayerInZone && Time.time > _shootTime)
        {
            if(_facingRight) Instantiate(missile, missileStartPoint.position, Quaternion.AngleAxis(-180, Vector3.right));
            if(!_facingRight) Instantiate(missile, missileStartPoint.position, Quaternion.AngleAxis(180, Vector3.up));
            _shootTime = Time.time + _shootRate;
        }

        if(transform.position.x > player.position.x && _facingRight) flip();
        if(transform.position.x < player.position.x && !_facingRight) flip();

        if(dragonHealth.currentHealth <= 0)
        {
            dragonAnim.SetBool("dragonDead", true);
            Destroy(gameObject, 0.8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _isPlayerDetected = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _isPlayerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _isPlayerInZone = false;
        }
    }

    private void flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
