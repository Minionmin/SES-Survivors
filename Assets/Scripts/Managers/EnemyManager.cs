using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary> ��������G�l�~�[�̃��X�g </summary>
    public List<Enemy> enemyList;

    private void Awake()
    {
        
    }

    private void Start()
    {
        // �e�G�l�~�[�ɓ|���ꂽ���̃C�x���g�Ɂu�����`�F�b�N�v��o�^
        foreach (Enemy enemy in enemyList)
        {
            enemy.OnEnemyDead += SpawnNextWave;
        }
    }

    /// <summary> �ēx�������ׂ����ǂ������m�F���� </summary>
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

    /// <summary> �ēx�������� </summary>
    private void SpawnNextWave()
    {
        // �������������Ȃ���΁i�S���|���ꂽ�j�������Ȃ�
        if (!ShouldRespawn()) return;

        foreach (Enemy enemy in enemyList)
        {
            enemy.Spawn();
        }
    }
}
