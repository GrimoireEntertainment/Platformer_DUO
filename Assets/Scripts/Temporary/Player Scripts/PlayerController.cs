using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //checkers
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundChecker;

    //Buttons
    [SerializeField] PressChecker rightButton;
    [SerializeField] PressChecker leftButton;

    //---------------------Wall Climbing/Jumping Variables----------------------

    [SerializeField] float wallClimbing = 3;
    [SerializeField] float movingDeniedTimeRate = 0.2f;
    [SerializeField] float wallCatchTimeRate = 0.5f;
    [SerializeField] Vector2 wallJumpDirection;
    [SerializeField] float wallJumpForce;

    public int facingDirection = 1;
    private bool wallTouch;
    private bool movingAllowed = true;
    private float movingDeniedTime;
    private float wallCatchTime;
    //--------------------------------------------------------------


    //--------------------------Stats--------------------------
    private float accelerationRate;
    public float maxSpeed;
    private float jumpHeight;
    private float secondJumpHeight;
    private bool canWallClimb = false;
    bool secondJumpAllowed;
    bool canJumpMore;
    //-------------------------------------------------------
    private float buttonSmooth = 0.0f;
    private float keyboardSmooth = 0.0f;
    private float groundCheckRadius = 0.05f;
    Rigidbody2D myRB;
    CapsuleCollider2D playerBody;
    GameObject AttackArea;
    Transform playerSize;
    bool facingRight;
    bool isGrounded;
    bool returnToNormalSize = false;
    bool keyboardCheck = true;
    bool buttonCheck = true;



    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        playerBody = gameObject.GetComponent<CapsuleCollider2D>();
        AttackArea = GameObject.Find("AttackArea");
        playerSize = GetComponentInChildren<Transform>();
        facingRight = true;
    }

    void Update()
    {
        Jumping(true);
        Slipping();
    }

    void FixedUpdate()
    {
        Moving();
        // CheckingWall();
    }

    public void UpdateStats()
    {
        Stats stats = GetComponentInChildren<Stats>();
        maxSpeed = stats.maxSpeed;
        accelerationRate = stats.acceleration;
        jumpHeight = stats.jumpHeight;
        secondJumpHeight = stats.secondJumpHeight;
        canWallClimb = stats.canWallClimb;
    }

    private void Moving()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, groundLayer);

        //part of code relates Wall Climbing
        if (canWallClimb && Time.time > movingDeniedTime)
        {
            movingAllowed = true;
        }

        CheckingPressedButtons();

        // Flipping character
        if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || rightButton.isPressed) && !facingRight) flip();
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || leftButton.isPressed) && facingRight) flip();
    }

    private void CheckingPressedButtons()
    {
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

        //--------------------------------Скольжение вниз по стенке------------------------------
        Sliding(leftOrRight, smoothing);
    }

    private void Sliding(sbyte leftOrRight, float smoothing)
    {
        if (canWallClimb && wallTouch && movingAllowed)
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
    }

    public void Jumping(bool isPC)  // Character jumping
    {
        /*if(player_y.activeSelf) */ secondJumping(isPC);
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

        //--------------------Прыжок во время лазанья по стенам-------------------
        WallJump(isPC);
    }

    private void secondJumping(bool isPC) {
        if(isGrounded) canJumpMore = true;

        if(!isGrounded) secondJumpAllowed = true;

        if(canJumpMore && secondJumpAllowed) {
            if( isPC && Input.GetKeyDown(KeyCode.Space)) {
                myRB.velocity = new Vector2(myRB.velocity.x, secondJumpHeight);
                canJumpMore = false;
            }
            else if(!isPC)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, secondJumpHeight);
                canJumpMore = false;
            }
        }
    }

    private void WallJump(bool isPC)
    {
        if (canWallClimb && isPC && wallTouch && Input.GetKeyDown(KeyCode.Space))
        {
            //Не уверен что лучше и правильнее Velocity или AddForce поэтому пусть пока на коментах постоит
            // Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            // myRB.AddForce(new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y), ForceMode2D.Force);
            myRB.velocity = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            movingDeniedTime = Time.time + movingDeniedTimeRate;
            movingAllowed = false;
        }
        else if (canWallClimb && !isPC && wallTouch)
        {
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

    private void Slipping() {

        if(Input.GetKey(KeyCode.S)) {
            playerBody.offset = new Vector2(0.0f, -0.25f);
            playerBody.size = new Vector2(1.5f, 1.0f);
            playerBody.direction = CapsuleDirection2D.Horizontal;

            AttackArea.SetActive(false);

            if(facingRight) playerSize.localScale = new Vector3(1.5f, 0.75f, 0.0f);
            if(!facingRight) playerSize.localScale = new Vector3(-1.5f, 0.75f, 0.0f);
        }
        else if(!Input.GetKey(KeyCode.S)) {
            playerBody.offset = new Vector2(0.0f, -0.0f);
            playerBody.size = new Vector2(1.0f, 1.5f);
            playerBody.direction = CapsuleDirection2D.Vertical;

            AttackArea.SetActive(true);

            if(facingRight) playerSize.localScale = new Vector3(1.0f, 1.0f, 0.0f);
            if(!facingRight) playerSize.localScale = new Vector3(-1.0f, 1.0f, 0.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (canWallClimb && gameObject.name == "Character") wallTouch = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canWallClimb && gameObject.name == "Character") wallCatchTime = Time.time + wallCatchTimeRate;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (canWallClimb && other.tag == "Wall" && !isGrounded && gameObject.name == "Character")
        {
            wallTouch = true;
        }
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
}
