using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionController : MonoBehaviour
{

    [SerializeField] float normalSpeed;
    [SerializeField] float turnAroundTime;
    private float startTime = 0.0f;
    bool facingRight = false;
    Rigidbody2D ScorpionRB;


    // Start is called before the first frame update
    void Start()
    {
        ScorpionRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        startTime = startTime + Time.deltaTime;
        if(facingRight) ScorpionRB.velocity = new Vector2(normalSpeed, ScorpionRB.velocity.y);
        if(!facingRight) ScorpionRB.velocity = new Vector2(-normalSpeed, ScorpionRB.velocity.y);

        if(startTime > turnAroundTime) {
            flip();
            startTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            
            

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
