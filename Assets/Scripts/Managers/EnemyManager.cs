using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary> 召喚するエネミーのリスト </summary>
    public List<Enemy> enemyList;

    private void Awake()
    {
        
    }

    private void Start()
    {
        // 各エネミーに倒された時のイベントに「召喚チェック」を登録
        foreach (Enemy enemy in enemyList)
        {
            enemy.OnEnemyDead += SpawnNextWave;
        }
    }

    /// <summary> 再度召喚すべきかどうかを確認する </summary>
    private bool ShouldRespawn()
    {
        foreach (Enemy enemy in enemyList)
        {
            if (!enemy.isDead)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary> 再度召喚する </summary>
    private void SpawnNextWave()
    {
        // 条件が満たさなければ（全部倒された）何もしない
        if (!ShouldRespawn()) return;

        foreach (Enemy enemy in enemyList)
        {
            enemy.Spawn();
        }
    }
}
