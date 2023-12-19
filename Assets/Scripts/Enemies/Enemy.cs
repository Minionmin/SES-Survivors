using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary> 敵のステータスデータを格納しているScriptable Object </summary>
    [SerializeField] protected CharacterData enemyData;

    private Vector3 moveDir;

    #region Stats
    /// <summary> 敵の体力 </summary>
    protected float hp;
    /// <summary> 敵の攻撃 </summary>
    protected float power;
    /// <summary> 敵の移動スピード </summary>
    protected float movespeed;
    /// <summary> 敵の回転スピード </summary>
    protected float rotateSpeed;
    /// <summary> プレイヤーとの最大の距離 </summary>
    protected float maxDistanceOffset;
    #endregion

    protected virtual void Awake()
    {
        // 敵のステータスを初期化
        InitializeStat();
    }

    protected virtual void LateUpdate()
    {
        // プレイヤーと重ならないように
        if (hasReached()) return;
        
        Chase();
    }

    /// <summary> 敵のステータスを初期化 </summary>
    protected virtual void InitializeStat()
    {
        if (enemyData != null)
        {
            hp = enemyData.hp;
            power = enemyData.power;
            movespeed = enemyData.movespeed;
            rotateSpeed = enemyData.rotateSpeed;
            maxDistanceOffset = enemyData.maxDistanceOffset;
        }
    }

    /// <summary> インプットの方向に回転 </summary>
    /// <param name="targetDir"> 目的の方向 </param>
    protected void Rotate(Vector3 targetDir)
    {
        Quaternion targetAngle = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotateSpeed);
    }

    /// <summary> プレイヤーを追いかける </summary>
    protected void Chase()
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
}
