using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    /// <summary> プレイヤーが経験値を得たイベント </summary>
    public static event Action OnExpGained;

    /// <summary> プレイヤーの参照 </summary>
    public Player player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

    /// <summary> プレイヤーの位置を取得 </summary>
    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

    /// <summary> プレイヤーに経験値を与える </summary>
    /// <param name="enemy"> 倒された敵 </param>
    public void GainPlayerEXP(Enemy enemy)
    {
        player.exp += enemy.dropEXP;

        // プレイヤーの経験値が達したらレベルを上げる
        if(IsLevelUp())
        {
            LevelPlayerUp();
        }

        // プレイヤが経験値を得たことに関する関数を呼ぶ
        OnExpGained?.Invoke();
    }

    /// <summary> プレイヤーが既にレベルアップかどうか </summary>
    public bool IsLevelUp()
    {
        return player.exp >= player.maxExp;
    }

    /// <summary> プレイヤーのレベルを上げる </summary>
    private void LevelPlayerUp()
    {
        // 越えた経験値を計算する
        var exceededExp =  player.exp - player.maxExp;

        // プレイヤーのレベルを上げる
        player.lvl++;
        player.exp = exceededExp;
        player.maxExp = CalculateMaxExp();
    }

    /// <summary> プレイヤーの次の最大経験値を計算する </summary>
    private int CalculateMaxExp()
    {
        return (int)(player.maxExp + Mathf.Pow((float)player.lvl, 1.5f));
    }
}
