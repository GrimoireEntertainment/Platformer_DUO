using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private float buttonSmooth = 0.0f;
    private float keyboardSmooth = 0.0f;
    [SerializeField] float rate;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundChecker;
    [SerializeField] PressChecker rightButton;
    [SerializeField] PressChecker leftButton;
    [SerializeField] PressChecker jumpButton;
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



    private void Moving()   // Function for moving character
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, groundLayer);

        // Condition for pressing left keyboard buttons

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {

            if(keyboardSmooth > 0 && keyboardCheck) {   // Condition for checking if player press button before keyboardSmooth reached zero
                keyboardSmooth = 0;
                keyboardCheck = false;
            }

            keyboardSmooth += rate;
            if(keyboardSmooth >= 1) keyboardSmooth = 1;

            myRB.velocity = Vector2.left * maxSpeed * keyboardSmooth;
        }

        // Condition for pressing right keyboard buttons

        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {

            if(keyboardSmooth > 0 && keyboardCheck) {   // Condition for checking if player press button before keyboardSmooth reached zero
                keyboardSmooth = 0;
                keyboardCheck = false;
            }
            
            keyboardSmooth += rate;
            if(keyboardSmooth >= 1) keyboardSmooth = 1;

            myRB.velocity = Vector2.right * maxSpeed * keyboardSmooth;
        }

        // Condition for not pressing left and right keyboard buttons

        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow)) {

            keyboardCheck = true;   // Enabling relevant condition

            keyboardSmooth -= rate;

            if(keyboardSmooth <= 0) keyboardSmooth = 0;
        }

        // Code for flipping character

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || rightButton.isPressed) && !facingRight) flip();
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || leftButton.isPressed) && facingRight)  flip();



        // Button Moving * Button Moving * Button Moving * Button Moving * Button Moving * 

        

        else if (leftButton.isPressed) {

            if(buttonSmooth > 0 && buttonCheck) {   // Condition for checking if player press button before keyboardSmooth reached zero
                buttonSmooth = 0;
                buttonCheck = false;
            }

            buttonSmooth += rate;

            if(buttonSmooth >= 1) buttonSmooth = 1;

            myRB.velocity = Vector2.left * maxSpeed * buttonSmooth;
        }

        else if (rightButton.isPressed) {

            if(buttonSmooth > 0 && buttonCheck) {   // Condition for checking if player press button before keyboardSmooth reached zero
                buttonSmooth = 0;
                buttonCheck = false;
            }

            buttonSmooth += rate;

            if(buttonSmooth >= 1) buttonSmooth = 1;

            myRB.velocity = Vector2.right * maxSpeed * buttonSmooth;
        }

        else if (!rightButton.isPressed && !leftButton.isPressed) {

            buttonCheck = true;    // Enabling relevant condition

            buttonSmooth -= rate;

            if(buttonSmooth <= 0) buttonSmooth = 0;
        }

        
    }

    private void Jumping()  // Function for character jumping
    {

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            isGrounded = false;
            myRB.AddForce(new Vector2(0, jumpHeight));
        }

        else if(isGrounded && jumpButton.isPressed) {
            isGrounded = false;
            myRB.AddForce(new Vector2(0, jumpHeight/1.95f));
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
