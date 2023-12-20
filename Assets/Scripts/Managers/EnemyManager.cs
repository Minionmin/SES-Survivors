using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary> ��������G�l�~�[�̃��X�g </summary>
    public List<Enemy> enemyList;

    /// <summary> �v���C���[����ő�̋��� </summary>
    [SerializeField] private float spawnMaxDistance;

    /// <summary> �v���C���[����Ŋ��̋��� </summary>
    [SerializeField] private float spawnMinDistance;

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
            RandomEnemyPosition(enemy);
            enemy.Spawn();
        }
    }

    /// <summary> �G�̈ʒu�������_������ </summary>
    private void RandomEnemyPosition(Enemy enemy)
    {
        float[] xz = new float[2];

        for(int i = 0 ; i < 2; i++)
        {
            // �����������_������
            xz[i] = Random.Range(spawnMinDistance, spawnMaxDistance);
            
            // +-�������_������
            if(Random.Range(0, 2) == 0)
            {
                xz[i] *= -1;
            }
        }

        // �v���C���[�̈ʒu���x�[�X�ɂ��ċ������v�Z����
        enemy.transform.position = GameManager.Instance.GetPlayerPos() + new Vector3(xz[0], 0.0f, xz[1]);
    }
}
