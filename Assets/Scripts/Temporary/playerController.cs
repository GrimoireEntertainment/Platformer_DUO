using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //Stats
    [SerializeField] float rate;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpHeight;

    //checkers
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundChecker;

    //Buttons
    [SerializeField] PressChecker rightButton;
    [SerializeField] PressChecker leftButton;
    [SerializeField] PressChecker jumpButton;

    private float buttonSmooth = 0.0f;
    private float keyboardSmooth = 0.0f;
    private float groundCheckRadius = 0.05f;
    Rigidbody2D myRB;
    bool facingRight;
    bool isGrounded;
    bool keyboardCheck = true;
    bool buttonCheck = true;

    float tempVar;


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


        // Pressing left keyboard buttons

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveCharachter(Vector2.left, ref keyboardSmooth, ref keyboardCheck);
        }

        // Pressing right keyboard buttons

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveCharachter(Vector2.right, ref keyboardSmooth, ref keyboardCheck);
        }

        // NOT pressing left and right keyboard buttons

        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow))
        {
            keyboardCheck = true;   // Enabling relevant condition

            keyboardSmooth -= rate;

            if (keyboardSmooth <= 0) keyboardSmooth = 0;
        }

        // Button Moving * Button Moving * Button Moving * Button Moving * Button Moving * 

        if (leftButton.isPressed)
        {
            MoveCharachter(Vector2.left, ref buttonSmooth, ref buttonCheck);
        }

        else if (rightButton.isPressed)
        {
            MoveCharachter(Vector2.right, ref buttonSmooth, ref buttonCheck);
        }

        else if (!rightButton.isPressed && !leftButton.isPressed)
        {
            buttonCheck = true;    // Enabling relevant condition

            buttonSmooth -= rate;

            if (buttonSmooth <= 0) buttonSmooth = 0;
        }

        // Flipping character

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || rightButton.isPressed) && !facingRight) flip();
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || leftButton.isPressed) && facingRight) flip();
    }


    private void MoveCharachter(Vector2 leftOrRight, ref float smoothing, ref bool checking)
    {
        if (smoothing > 0 && checking) // If player press button before keyboardSmooth reached zero
        {
            smoothing = 0;
            checking = false;
        }

        smoothing += rate;
        if (smoothing >= 1) smoothing = 1;

        myRB.velocity = leftOrRight * maxSpeed * smoothing;
    }

    private void Jumping()  // Character jumping
    {

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            myRB.AddForce(new Vector2(0, jumpHeight));
        }

        else if (isGrounded && jumpButton.isPressed)
        {
            isGrounded = false;
            myRB.AddForce(new Vector2(0, jumpHeight / 1.95f));
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
