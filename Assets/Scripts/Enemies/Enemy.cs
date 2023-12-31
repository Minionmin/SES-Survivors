using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovement, IDamageable
{
    /// <summary> 敵が倒された時に打つイベント </summary>
    public event Action OnEnemyDead;

    /// <summary> 敵のステータスデータを格納しているScriptable Object </summary>
    [SerializeField] protected EnemyData enemyData;

    /// <summary> 移動方向 </summary>
    protected Vector3 moveDir;

    #region Component
    /// <summary> 敵の当たり判定 </summary>
    [SerializeField] protected SphereCollider sphereCollider;

    /// <summary> 敵のアニメーションコントローラ </summary>
    public Animator animator;

    /// <summary> 死亡アニメーショントリガー </summary>
    protected const string DIE = "Die";

    /// <summary> 敵のメッシュレンダラー </summary>
    [SerializeField] protected Renderer bodyRenderer;

    /// <summary> 敵メッシュのマテリアル </summary>
    protected Material material;
    #endregion

    #region Flag
    /// <summary> 敵が動けるかどうか </summary>
    protected bool canMove => !isDead;

    /// <summary> 敵が倒されたかどうか </summary>
    public bool isDead = false;
    #endregion

    #region Stats
    /// <summary> 敵の最大体力 </summary>
    protected int maxHp;
    /// <summary> 敵の体力 </summary>
    protected int hp;
    /// <summary> 敵の攻撃 </summary>
    protected float power;
    /// <summary> 敵の移動スピード </summary>
    protected float movespeed;
    /// <summary> 敵の回転スピード </summary>
    protected float rotateSpeed;
    /// <summary> プレイヤーとの最大の距離 </summary>
    protected float maxDistanceOffset;
    /// <summary> ドロップする経験値 </summary>
    public int dropEXP {  get; protected set; }
    #endregion

    protected virtual void Awake()
    {
        // 敵のステータスを初期化
        InitializeStat();

        // マテリアルの複製
        material = bodyRenderer.material;
    }

    protected virtual void LateUpdate()
    {
        // プレイヤーと重ならないように
        // 動けない時には何もしない
        if (hasReached() || !canMove) return;
        
        Move();
    }

    /// <summary> 敵のステータスを初期化 </summary>
    protected virtual void InitializeStat()
    {
        if (enemyData != null)
        {
            maxHp = enemyData.hp;
            hp = enemyData.hp;
            power = enemyData.power;
            movespeed = enemyData.movespeed;
            rotateSpeed = enemyData.rotateSpeed;
            maxDistanceOffset = enemyData.maxDistanceOffset;
            dropEXP = enemyData.dropEXP;
        }
    }

    /// <summary> インプットの方向に回転 </summary>
    /// <param name="targetDir"> 目的の方向 </param>
    public virtual void Rotate(Vector3 targetDir)
    {
        Quaternion targetAngle = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotateSpeed);
    }

    /// <summary> プレイヤーを追いかける </summary>
    public virtual void Move()
    {
        // 移動方向を決める
        moveDir = (GameManager.Instance.player.transform.position - transform.position).normalized;

        // 移動する
        transform.position += moveDir * movespeed * Time.deltaTime;

        // 移動方向に応じて回転する
        Rotate(moveDir);
    }

    /// <summary> プレイヤーに近づけたかどうかをチェックする </summary>
    protected bool hasReached()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= maxDistanceOffset;
    }

    /// <summary> ダメージをもらう処理 </summary>
    public virtual void TakeDamage(int damage)
    {
        // ダメージを計算する
        hp -= damage;

        // 体力がマイナスにならないように
        hp = Mathf.Clamp(hp, 0, maxHp);

        // 体力が0になったら死亡状態の処理をする
        if(hp == 0)
        {
            // 死亡処理
            Die();
        }
    }

    /// <summary> 死亡処理 </summary>
    public virtual void Die()
    {
        // 死亡フラグを立てる
        isDead = true;

        // プレイヤーに経験値を与える
        GameManager.Instance.GainPlayerEXP(this);
    }

    /// <summary> 倒された敵を非表示する </summary>
    protected virtual void Hide()
    {
        // 影を落とさないようにする
        bodyRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        // モデルを非表示にする
        bodyRenderer.enabled = false;

        // 当たり判定を無効化にする
        sphereCollider.enabled = false;

        // 敵が倒されたイベントを打つ
        OnEnemyDead?.Invoke();
    }

    /// <summary> 倒された敵を再度表示する </summary>
    public virtual void Spawn()
    {
        // ステータスを基に戻す
        InitializeStat();

        // 死亡フラグを下ろす
        isDead = false;

        // 影を落とす
        bodyRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        // モデルを表示にする
        bodyRenderer.enabled = true;

        // 当たり判定を有効化にする
        sphereCollider.enabled = true;
    }
}
