using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary> 召喚するエネミーのリスト </summary>
    public List<Enemy> enemyList;

    /// <summary> プレイヤーから最大の距離 </summary>
    [SerializeField] private float spawnMaxDistance;

    /// <summary> プレイヤーから最寄りの距離 </summary>
    [SerializeField] private float spawnMinDistance;

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
            RandomEnemyPosition(enemy);
            enemy.Spawn();
        }
    }

    /// <summary> 敵の位置をランダムする </summary>
    private void RandomEnemyPosition(Enemy enemy)
    {
        float[] xz = new float[2];

        for(int i = 0 ; i < 2; i++)
        {
            // 距離をランダムする
            xz[i] = Random.Range(spawnMinDistance, spawnMaxDistance);
            
            // +-をランダムする
            if(Random.Range(0, 2) == 0)
            {
                xz[i] *= -1;
            }
        }

        // プレイヤーの位置をベースにして距離を計算する
        enemy.transform.position = GameManager.Instance.GetPlayerPos() + new Vector3(xz[0], 0.0f, xz[1]);
    }
}
