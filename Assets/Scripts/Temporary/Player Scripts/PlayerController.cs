using UnityEngine;
using System.Collections;

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

//__________________Wall Climbing/Jumping Variables_____________________________

    [SerializeField] float wallClimbing = 3;
    [SerializeField] float movingDeniedTimeRate = 0.2f;
    [SerializeField] float wallCatchTimeRate = 0.5f;
    [SerializeField] Vector2 wallJumpDirection;
    [SerializeField] float wallJumpForce;

    private bool wallTouch;
    private int facingDirection = 1;
    private bool movingAllowed = true;
    private float movingDeniedTime;
    private float wallCatchTime;
//_________________________________________________________________________________
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
        // CheckingWall();
    }



    private void Moving()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, groundLayer);
        if (Time.time > movingDeniedTime)
        {
            movingAllowed = true;
        }
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
        if (movingAllowed) myRB.velocity = new Vector2(leftOrRight * maxSpeed * smoothing, myRB.velocity.y);

//_______________________Движение по стенам_________________________________________
        if (wallTouch && movingAllowed)
        {
            if (Time.time < wallCatchTime)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, 1.5f);
            }
            else
            {
                myRB.velocity = new Vector2(leftOrRight * maxSpeed * smoothing, -wallClimbing);
            }
        }
//_______________________________________________________________________________
    }

    public void Jumping(bool isPC)  // Character jumping
    {

        if (isPC && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            // myRB.AddForce(new Vector2(myRB.velocity.x, jumpHeight));
            myRB.velocity = new Vector2(myRB.velocity.x, jumpHeight);
        }

        else if (!isPC && isGrounded)    // проверяю это через комп или нет и в компоненте кнопки вызываю эту функцию
        {
            isGrounded = false;
            //Не уверен что лучше и правильнее Velocity или AddForce поэтому пусть пока на коментах постоит
            // myRB.AddForce(new Vector2(myRB.velocity.x, jumpHeight));
            myRB.velocity = new Vector2(myRB.velocity.x, jumpHeight);
        }

//__________________________________Прыжок во время лазанья по стенам_____________________________________________________
        else if (wallTouch && Input.GetKeyDown(KeyCode.Space))
        {
            //Не уверен что лучше и правильнее Velocity или AddForce поэтому пусть пока на коментах постоит
            // Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            // myRB.AddForce(new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y), ForceMode2D.Force);
            myRB.velocity = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            movingDeniedTime = Time.time + movingDeniedTimeRate;
            movingAllowed = false;
        }
    }

    private void flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    // private void CheckingWall()
    // {
    //     RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right * facingDirection, 0.6f);
    //     foreach (RaycastHit2D hit in hits)
    //     {
    //         if(hit.collider.tag == "Wall")
    //         {
    //             print(hit.collider.name);
    //             if (!isGrounded)
    //             {
    //                 wallTouch = true;
    //             }
    //             else
    //             {
    //                 wallTouch = false;
    //             }
    //         }
    //     }
    // }




    private void OnTriggerExit2D(Collider2D other)
    {
        if(gameObject.name == "Character") wallTouch = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.name == "Character")wallCatchTime = Time.time + wallCatchTimeRate;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Wall" && !isGrounded && gameObject.name == "Character")
        {
            wallTouch = true;
        }
    }
}
