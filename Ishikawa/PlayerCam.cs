using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;  // プレイヤーの回転を管理
    public Transform camHolder;    // カメラを保持するオブジェクト

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // マウスの入力を取得
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // カメラの回転
        yRotation += mouseX;
        xRotation -= mouseY;

        // 垂直方向の回転制限
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // カメラの回転を適用
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // プレイヤーの回転（カメラのY軸回転に合わせる）
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
