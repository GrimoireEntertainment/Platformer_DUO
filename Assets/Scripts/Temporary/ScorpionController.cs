using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionController : MonoBehaviour
{

    [SerializeField] float normalSpeed;
    // [SerializeField] float turnAroundTime;
    [SerializeField] float scorpionWalkingArea;
    Vector3 pointOfOrigin;
    [SerializeField] float scorpionMaxHealth;
    [SerializeField] float scorpionDamage;
    float scorpionHealth;


    [SerializeField] PlayerDetectionScript PlayerDetection;
    [SerializeField] GameObject player;
    [SerializeField] playerController playerCtrl;

    private float startTime = 0.0f;
    bool facingRight = false;
    Rigidbody2D ScorpionRB;


    // Start is called before the first frame update
    void Start()
    {
        ScorpionRB = GetComponent<Rigidbody2D>();
        scorpionHealth = scorpionMaxHealth;
        pointOfOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(facingRight) ScorpionRB.velocity = new Vector2(normalSpeed, ScorpionRB.velocity.y);
        if(!facingRight) ScorpionRB.velocity = new Vector2(-normalSpeed, ScorpionRB.velocity.y);

        // if(Mathf.Abs(transform.position.x - pointOfOrigin.x) > scorpionWalkingArea) {
        //     flip();
        // }

        if(transform.position.x > pointOfOrigin.x + scorpionWalkingArea) {
            flip();
            pointOfOrigin.x += 1;
        }

        if(transform.position.x < pointOfOrigin.x - scorpionWalkingArea) {
            flip();
            pointOfOrigin.x -= 1;
        }

        if(PlayerDetection.playerDetected) {

            if(player.transform.position.x < transform.position.x ) {
                if(facingRight) flip();
                ScorpionRB.velocity = new Vector2(-2 * normalSpeed, ScorpionRB.velocity.y);
            }

            if(player.transform.position.x > transform.position.x) {
                if(!facingRight) flip();
                ScorpionRB.velocity = new Vector2(2 * normalSpeed, ScorpionRB.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            
            playerCtrl.playerHealth -= scorpionDamage;

        }
    }
    
    private void flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
