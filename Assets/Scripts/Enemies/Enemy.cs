using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary> 敵のステータスデータを格納しているScriptable Object </summary>
    [SerializeField] protected CharacterData enemyData;

    #region Stats
    /// <summary> 敵の体力 </summary>
    protected float hp;
    /// <summary> 敵の攻撃 </summary>
    protected float power;
    /// <summary> 敵の移動スピード </summary>
    protected float movespeed;
    /// <summary> 敵の回転スピード </summary>
    protected float rotateSpeed;
    #endregion

    protected virtual void Awake()
    {
        // 敵のステータスを初期化
        InitializeStat();
    }



    /// <summary> プレイヤーのステータスを初期化 </summary>
    protected virtual void InitializeStat()
    {
        if (enemyData != null)
        {
            hp = enemyData.hp;
            power = enemyData.power;
            movespeed = enemyData.movespeed;
            rotateSpeed = enemyData.rotateSpeed;
        }
    }
}
