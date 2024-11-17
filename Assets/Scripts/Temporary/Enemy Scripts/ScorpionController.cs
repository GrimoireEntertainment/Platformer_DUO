using UnityEngine;

public class ScorpionController : MonoBehaviour
{

    [SerializeField] float normalSpeed;
    [SerializeField] float scorpionWalkingArea;
    Vector3 pointOfOrigin;

    PlayerDetectionScript PlayerDetection;
    GameObject player;
    Health playerHealth;
    Animator myAnim;

    bool facingRight = false;
    Rigidbody2D ScorpionRB;


    void Start()
    {
        PlayerDetection = GetComponentInChildren<PlayerDetectionScript>();
        ScorpionRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        pointOfOrigin = transform.position;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(facingRight) ScorpionRB.velocity = new Vector2(normalSpeed, ScorpionRB.velocity.y);
        if(!facingRight) ScorpionRB.velocity = new Vector2(-normalSpeed, ScorpionRB.velocity.y);

        if(transform.position.x > pointOfOrigin.x + scorpionWalkingArea) {
            flip();
            pointOfOrigin.x += 1;
        }

        if(transform.position.x < pointOfOrigin.x - scorpionWalkingArea) {
            flip();
            pointOfOrigin.x -= 1;
        }

        if(PlayerDetection.playerDetected) 
        {
            myAnim.SetBool("isRunning", true);
            if(player.transform.position.x < transform.position.x ) {
                if(facingRight) flip();
                ScorpionRB.velocity = new Vector2(-3 * normalSpeed, ScorpionRB.velocity.y);
            }

            if(player.transform.position.x > transform.position.x) {
                if(!facingRight) flip();
                ScorpionRB.velocity = new Vector2(3 * normalSpeed, ScorpionRB.velocity.y);
            }
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
