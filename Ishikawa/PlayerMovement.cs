using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundDrag;

    public float playerheight;
    public LayerMask Ground;
    bool grounded;

    public Transform orientation;

    private float HorizontalInput;
    private float VerticalInput;
    private float moveSpeed = 5f; // プレイヤーの移動速度
    public float sprintSpeed; //走るときのスピード
    public float WalkSpeed; //歩く時のスピード
    public MovementState state;

    public float JumpForce;//ジャンプ力の値
    public float JumpCoolDown;//ジャンプのクールダウン
    public float airMultiplier;//空中での操作性を少し悪くするための値
    bool ReadyToJump;//ジャンプができるかできないかのための真偽値

    Vector3 moveDirection;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ReadyToJump = true;
    }

   
    void Update()
    {
        //地面と接しているかを判断
        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, Ground);

        //接している場合は、設定した減速値を代入しプレイヤーを滑りにくくする
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        ProcessInput();
        SpeedControl();
        StateHandler();
    }


    private void FixedUpdate()
    {
        movePlayer();
    }

private void ProcessInput()
{
    // 入力を取得（-1 から 1 の範囲）
    HorizontalInput = Input.GetAxisRaw("Horizontal");
    VerticalInput = Input.GetAxisRaw("Vertical");

    // 入力に基づく移動ベクトルを計算
    Vector3 movement = new Vector3(HorizontalInput, 0f, VerticalInput).normalized;

    // プレイヤーが入力に基づいて移動する処理
    if (movement.magnitude >= 0.1f)
    {
        // 移動方向に基づいてプレイヤーを移動
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    if (Input.GetKeyDown(KeyCode.Space) && grounded && ReadyToJump)
        {
            ReadyToJump = false;

            Jump();

            Invoke(nameof(resetJump), JumpCoolDown);//JumpCoolDownで設定した秒数後にresetJump関数を呼び出す。
        }
}


    private void movePlayer()
{
    // カメラの向きに基づく移動方向
    moveDirection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;

    // 地面にいる場合の移動
    if (grounded)
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    else
    {
        // 空中での移動（空中操作の補正）
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
}



    private void SpeedControl()
    {
        //プレイヤーのスピードを制限
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
{
    // XZ 平面の速度を完全リセット
    rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
    
    // ジャンプ力を加える
    rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

    Debug.Log("ジャンプしました！");
}

    private void resetJump()
    {
        ReadyToJump = true;
    }

    public enum MovementState
    {
        walking,　//歩いている状態
        sprinting, //走っている状態
        air //空中にいる状態
    }

     private void StateHandler()
    {
        //左シフトを押しているかつ地面と接している場合は、ステートをsprintingにして走る
        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        //ただ移動ボタンを押している場合は、ステートをwalkingにして歩く
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = WalkSpeed;
        }

        else
        {
            state = MovementState.air;
        }
    }
}