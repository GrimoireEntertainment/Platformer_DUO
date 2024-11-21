using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ControllerInput : MonoBehaviour
{
    public static ControllerInput Instance;

    public GameObject btnJetpack;
    public GameObject btnSlide;

    CanvasGroup canvasGroup;

    //[Header("---KEYBOARD CONTROLLER---")]
    //public KeyCode key_left;
    //public KeyCode key_right;
    //public KeyCode key_down;
    //public KeyCode key_up;
    //public KeyCode key_A;
    //public KeyCode key_B;
    //public KeyCode key_Y;
    //public KeyCode key_X;
    //public KeyCode key_Jetpack = KeyCode.F;
    //public KeyCode key_Throw = KeyCode.R;
    //public KeyCode key_Melee = KeyCode.C;

    GameplayControl controls;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            StopMove(0);

        controls.PlayerControl.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerControl.Disable();

    }

    private void Awake()
    {
        Instance = this;

        controls = new GameplayControl();
        controls.PlayerControl.B.started += ctx => Jump();
        controls.PlayerControl.B.canceled += ctx => JumpOff();

        controls.PlayerControl.Y.started += ctx => MeleeAttack();
        controls.PlayerControl.X.started += ctx => RangeAttack();
        controls.PlayerControl.Sliding.started += ctx => SlideOn();

        controls.PlayerControl.A.started += ctx => ActionBegin();
        controls.PlayerControl.A.canceled += ctx => ActionCancel();

        controls.PlayerControl.DLeft.started += ctx => MoveLeft();
        controls.PlayerControl.DLeft.started += ctx => MoveLeftTap();
        controls.PlayerControl.DLeft.canceled += ctx => StopMove(-1);

        controls.PlayerControl.DRight.started += ctx => MoveRight();
        controls.PlayerControl.DRight.started += ctx => MoveRightTap();
        controls.PlayerControl.DRight.canceled += ctx => StopMove(1);

        controls.PlayerControl.DUp.started += ctx => MoveDUp();
        controls.PlayerControl.DUp.canceled += ctx => StopMove(0);

        controls.PlayerControl.DDown.started += ctx => MoveDown();
        controls.PlayerControl.DDown.canceled += ctx => StopMove(0);

        controls.PlayerControl.Jetpack.started += ctx => UseJetpack(true);
        controls.PlayerControl.Jetpack.canceled += ctx => UseJetpack(false);

        controls.PlayerControl.Jetpack.started += ctx => UseJetpack(true);
        controls.PlayerControl.Jetpack.canceled += ctx => UseJetpack(false);
    }

    private void Update()
    {
        btnJetpack.SetActive(GameManager.Instance.Player.isJetpackActived);
        btnSlide.SetActive(GameManager.Instance.Player.isRunning);



        //if (Input.GetKeyDown(key_Jetpack))
        //    UseJetpack(true);
        //else if (Input.GetKeyUp(key_Jetpack))
        //    UseJetpack(false);

        //if (Input.GetKeyDown(key_Throw))
        //    RangeAttack();

        //if (Input.GetKeyDown(key_Melee))
        //    MeleeAttack();

        //if (Input.GetKeyDown(key_B))
        //    Jump();
        //if (Input.GetKeyUp(key_B))
        //    JumpOff();

        //if (Input.GetKeyDown(key_Y))
        //    MeleeAttack();

        //if (Input.GetKeyDown(key_X))
        //    RangeAttack();

        //if (Input.GetKeyDown(key_A))
        //    ActionBegin();
        //if (Input.GetKeyUp(key_A))
        //    ActionCancel();

        //if (Input.GetKeyDown(key_left))
        //{
        //    MoveLeft();
        //    MoveLeftTap();
        //}
        //if (Input.GetKeyUp(key_left))
        //    StopMove(-1);

        //if (Input.GetKeyDown(key_right))
        //{
        //    MoveRight();
        //    MoveRightTap();
        //}
        //if (Input.GetKeyUp(key_right))
        //    StopMove(1);

        //if (Input.GetKeyDown(key_up))
        //    MoveDUp();
        //if (Input.GetKeyUp(key_up))
        //    StopMove(0);

        //if (Input.GetKeyDown(key_down))
        //    MoveDown();
        //if (Input.GetKeyUp(key_down))
        //    StopMove(0);

        if (GameManager.Instance.Player.isRunning && Input.GetKeyDown(KeyCode.LeftShift))
        {
            SlideOn();
        }
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowController(bool show)
    {
        if (show)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }

    [ReadOnly] public bool allowJump = true;
    [ReadOnly] public bool allowSlide = true;

    public void Jump()
    {
        if (allowJump)
            GameManager.Instance.Player.Jump();
    }

    public void JumpOff()
    {
        if (allowJump)
            GameManager.Instance.Player.JumpOff();
    }

    public void MoveLeft()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.MoveLeft();
        }
    }

    public void MoveLeftTap()
    {
        GameManager.Instance.Player.MoveLeftTap();
    }

    public void MoveRight()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.MoveRight();
        }
    }

    public void MoveRightTap()
    {
        GameManager.Instance.Player.MoveRightTap();
    }

    public void MoveDUp()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.MoveUp();
        }
    }

    public void MoveDown()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.MoveDown();
        }
    }

    public void StopMove(int fromDirection)
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.StopMove(fromDirection);
        }
    }

    public void RangeAttack()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.RangeAttack();
        }
    }

    public void MeleeAttack()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing) 
            GameManager.Instance.Player.MeleeAttack();
    }

    public void ActionBegin()
    {
        GameManager.Instance.Player.Action(true);
    }

    public void ActionCancel()
    {
        GameManager.Instance.Player.Action(false);
    }


    public void UseJetpack(bool use)
    {
        if (use && !GameManager.Instance.Player.isJetpackActived)
            return;

        GameManager.Instance.Player.UseJetpack(use);
    }

    public void SlideOn()
    {
        if (GameManager.Instance.Player.isRunning)
        {
            if (allowSlide)
                GameManager.Instance.Player.SlideOn();
        }
    }
}
