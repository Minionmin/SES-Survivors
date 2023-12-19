using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary> プレイヤーのインプットを処理するクラス </summary>
    [SerializeField] private PlayerControl playerControl;

    /// <summary> プレイヤーのステータスデータを格納しているScriptable Object </summary>
    [SerializeField] protected PlayerData playerData;

    /// <summary> プレイヤーのアニメーションコントローラ </summary>
    [SerializeField] protected Animator animator;

    #region Flag
    /// <summary> プレイヤーが移動しているかどうか </summary>
    protected bool isMoving = false;
    #endregion

    #region AnimatorParameter
    protected const string IS_MOVING = "IsMoving"; // 移動しているかどうか
    protected const string Attack = "Attack"; // 攻撃トリガー
    protected const string Die = "Die"; // 死亡トリガー
    #endregion

    #region Stats
    /// <summary> プレイヤーの体力 </summary>
    protected float hp;
    /// <summary> プレイヤーのスタミナ </summary>
    protected float stamina;
    /// <summary> プレイヤーの攻撃パワー </summary>
    protected float power;
    /// <summary> プレイヤーの移動スピード </summary>
    protected float movespeed;
    /// <summary> プレイヤーの回転スピード </summary>
    protected float rotateSpeed = 4;
    #endregion

    protected virtual void Awake()
    {
        // プレイヤーのステータスを初期化
        InitializeStat();
    }

    protected virtual void Start()
    {
        // カメラの注視対象をセット（このオブジェクト）
        CameraManager.Instance.SetCameraTarget(transform);
    }

    protected virtual void Update()
    {
    }

    protected virtual void LateUpdate()
    {
        // 移動する
        Move();
    }

    /// <summary> 移動 </summary>
    protected void Move()
    {
        // 方向をゲット
        var dir = playerControl.GetMoveDirection();

        // 移動しているかどうかを確認
        if (dir != Vector3.zero)
        {
            // 移動しているフラグを立てる
            isMoving = true;

            // プレイヤーを任意の方向に移動する
            transform.position += dir * movespeed * Time.deltaTime;

            Rotate(dir);

            // Idle状態から移動したらアニメーションをセットする
            if (isMoving && !animator.GetBool(IS_MOVING))
            {
                animator.SetBool(IS_MOVING, true);
            }
        }
        else
        {
            // 移動していなければそれに関するフラグをセットする
            isMoving = false;
            animator.SetBool(IS_MOVING, false);
        }
    }

    protected void Rotate(Vector3 targetDir)
    {
        Quaternion targetAngle = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotateSpeed);
    }

    /// <summary> プレイヤーのステータスを初期化 </summary>
    protected virtual void InitializeStat()
    {
        if(playerData != null)
        {
            hp = playerData.hp;
            stamina = playerData.stamina;
            power = playerData.power;
            movespeed = playerData.movespeed;
        }
    }
}
