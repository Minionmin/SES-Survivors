using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMovement
{
    /// <summary> プレイヤーのインプットを処理するクラス </summary>
    [SerializeField] private PlayerControl playerControl;

    /// <summary> プレイヤーのステータスデータを格納しているScriptable Object </summary>
    [SerializeField] protected PlayerData playerData;

    /// <summary> プレイヤーのアニメーションコントローラ </summary>
    public Animator animator;

    /// <summary> 攻撃のスタートポイント </summary>
    public Transform bulletSpawnPoint;

    #region Flag
    /// <summary> プレイヤーが移動しているかどうか </summary>
    protected bool isMoving = false;
    /// <summary> プレイヤーが攻撃できるかどうか </summary>
    protected bool canAttack = false;
    #endregion

    #region AnimatorParameter
    protected const string IS_MOVING = "IsMoving"; // 移動しているかどうか
    protected const string DIE = "Die"; // 死亡トリガー
    #endregion

    #region Stats
    /// <summary> プレイヤーのレベル </summary>
    public int lvl;//[HideInInspector] 
    /// <summary> プレイヤーの現在のExp </summary>
    public int exp;//[HideInInspector] 
    /// <summary> プレイヤーの最大Exp </summary>
    public int maxExp;//[HideInInspector] 
    /// <summary> プレイヤーの体力 </summary>
    protected int hp;
    /// <summary> プレイヤーのスタミナ </summary>
    protected float stamina;
    /// <summary> プレイヤーの移動スピード </summary>
    protected float movespeed;
    /// <summary> プレイヤーの回転スピード </summary>
    protected float rotateSpeed;
    /// <summary> プレイヤーの最大攻撃間隔 </summary>
    protected float attackCooldownMax;
    /// <summary> プレイヤーの攻撃間隔 </summary>
    protected float attackCooldown;
    #endregion

    protected virtual void Awake()
    {
        // プレイヤーのステータスを初期化
        InitializeStat();

        // 攻撃間隔の初期化
        attackCooldown = attackCooldownMax;
    }

    protected virtual void Start()
    {
        // カメラの注視対象をセット（このオブジェクト）
        CameraManager.Instance.SetCameraTarget(transform);
    }

    protected virtual void Update()
    {
        UpdateAttack();
    }

    /// <summary> 一定の間隔で攻撃する </summary>
    private void UpdateAttack()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0)
        {
            // 攻撃できるフラグを立てる
            canAttack = true;

            // 攻撃タイマーをリセット
            attackCooldown = attackCooldownMax;
        }
    }

    protected virtual void LateUpdate()
    {
        // 移動する
        Move();
    }

    /// <summary> プレイヤーのステータスを初期化 </summary>
    protected virtual void InitializeStat()
    {
        if(playerData != null)
        {
            lvl = 1;
            exp = 0;
            maxExp = playerData.maxEXP;
            hp = playerData.hp;
            stamina = playerData.stamina;
            movespeed = playerData.movespeed;
            rotateSpeed = playerData.rotateSpeed;
            attackCooldownMax = playerData.attackCooldownMax;
        }
    }

    /// <summary> インプットされた方向に回転 </summary>
    /// <param name="targetDir"> 目的の方向 </param>
    public void Rotate(Vector3 targetDir)
    {
        Quaternion targetAngle = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotateSpeed);
    }

    /// <summary> 移動 </summary>
    public void Move()
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

            // 移動している方向に回転
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
}
