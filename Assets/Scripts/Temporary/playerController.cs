using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField] float maxSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundChecker;
    [SerializeField] PressChecker rightButton;
    [SerializeField] PressChecker leftButton;
    private float groundCheckRadius = 0.05f;
    Rigidbody2D myRB;
    bool facingRight;
    bool isGrounded;
    
    
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    void Update()
    {
        Jumping();
    }

    void FixedUpdate()
    {
        Moving();
    }



    private void Moving()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, groundLayer);

        float move = Input.GetAxis("Horizontal");

        myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);

        if (move > 0 && !facingRight) flip();
        if (move < 0 && facingRight)  flip();


        // Button Moving

        if (leftButton.isPressed) {
            transform.position += Vector3.left * maxSpeed * Time.deltaTime;
        }
        else if (rightButton.isPressed) {
            transform.position += Vector3.right * maxSpeed * Time.deltaTime;
        }
    }

    private void Jumping()
    {

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            myRB.AddForce(new Vector2(0, jumpHeight));
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
