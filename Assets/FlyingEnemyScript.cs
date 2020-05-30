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
    private float _shootTime = 0.0f;
    private bool _isPlayerDetected = false;
    private bool _isPlayerInZone = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
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
            Instantiate(missile, missileStartPoint.position, transform.rotation);
            _shootTime = Time.time + _shootRate;
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
}
