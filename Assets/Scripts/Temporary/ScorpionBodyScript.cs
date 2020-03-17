using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionBodyScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float bounceForce;
    Rigidbody2D playerRB;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {

            playerRB = player.GetComponent<Rigidbody2D>();

            if(transform.position.x > player.transform.position.x) playerRB.AddForce(new Vector2(-bounceForce, bounceForce));
            if(transform.position.x < player.transform.position.x) playerRB.AddForce(new Vector2(bounceForce, bounceForce));
            

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
