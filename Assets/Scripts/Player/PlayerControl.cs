using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    /// <summary> インプット処理クラス </summary>
    private InputControl inputControl;

    /// <summary> プレイヤーの移動方向 </summary>
    private Vector2 moveDir;

    private void Awake()
    {
        // インプット処理クラスを生成
        inputControl = new InputControl();

        // インプット処理クラスを有効化
        inputControl.Player.Enable();
    }

    private void Update()
    {
        // キーボード・スティックからインプット値を読む
        moveDir = inputControl.Player.Move.ReadValue<Vector2>();
    }

    /// <summary> プレイヤーの移動方向Vector3をゲット </summary>
    public Vector3 GetMoveDirection()
    {
        return new Vector3(moveDir.x, 0.0f, moveDir.y).normalized;
    }
}
