﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Stats
    [SerializeField] float accelerationRate;
    // [SerializeField] float breakRate;
    public float maxSpeed;
    [SerializeField] float jumpHeight;

    //checkers
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundChecker;

    //Buttons
    [SerializeField] PressChecker rightButton;
    [SerializeField] PressChecker leftButton;

    private float buttonSmooth = 0.0f;
    private float keyboardSmooth = 0.0f;
    private float groundCheckRadius = 0.05f;
    Rigidbody2D myRB;
    bool facingRight;
    bool isGrounded;
    bool keyboardCheck = true;
    bool buttonCheck = true;



    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    void Update()
    {
        Jumping(true);
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
            MoveCharacter(-1, ref keyboardSmooth, ref keyboardCheck);
        }

        // Pressing right keyboard buttons

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveCharacter(1, ref keyboardSmooth, ref keyboardCheck);
        }

        // NOT pressing left and right keyboard buttons

        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow))
        {
            keyboardCheck = true;   // Enabling relevant condition

            keyboardSmooth -= accelerationRate;

            if (keyboardSmooth <= 0) keyboardSmooth = 0;
        }

        // Button Moving * Button Moving * Button Moving * Button Moving * Button Moving * 

        if (leftButton.isPressed)
        {
            MoveCharacter(-1, ref buttonSmooth, ref buttonCheck);
        }

        else if (rightButton.isPressed)
        {
            MoveCharacter(1, ref buttonSmooth, ref buttonCheck);
        }

        else if (!rightButton.isPressed && !leftButton.isPressed)
        {
            buttonCheck = true;    // Enabling relevant condition

            buttonSmooth -= accelerationRate;

            if (buttonSmooth <= 0) buttonSmooth = 0;
        }

        // Flipping character

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || rightButton.isPressed) && !facingRight) flip();
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || leftButton.isPressed) && facingRight) flip();
    }


    private void MoveCharacter(sbyte leftOrRight, ref float smoothing, ref bool checking)
    {
        if (smoothing > 0 && checking) // If player press button before keyboardSmooth reached zero
        {
            smoothing = 0;
            checking = false;
        }

        smoothing += accelerationRate;
        if (smoothing >= 1) smoothing = 1;

        myRB.velocity = new Vector2(leftOrRight * maxSpeed * smoothing, myRB.velocity.y);
    }

    public void Jumping(bool isPC)  // Character jumping
    {

        if (isPC && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            myRB.AddForce(new Vector2(0, jumpHeight));
        }

        else if (!isPC && isGrounded)    // проверяю это через комп или нет и в компоненте кнопки вызываю эту функцию
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