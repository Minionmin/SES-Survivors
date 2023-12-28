using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    /// <summary> �v���C���[���o���l�𓾂��C�x���g </summary>
    public static event Action OnExpGained;

    /// <summary> �v���C���[�̎Q�� </summary>
    public Player player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

    /// <summary> �v���C���[�̈ʒu���擾 </summary>
    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

    /// <summary> �v���C���[�Ɍo���l��^���� </summary>
    /// <param name="enemy"> �|���ꂽ�G </param>
    public void GainPlayerEXP(Enemy enemy)
    {
        player.exp += enemy.dropEXP;

        // �v���C���[�̌o���l���B�����烌�x�����グ��
        if(IsLevelUp())
        {
            LevelPlayerUp();
        }

        // �v���C�����o���l�𓾂����ƂɊւ���֐����Ă�
        OnExpGained?.Invoke();
    }

    /// <summary> �v���C���[�����Ƀ��x���A�b�v���ǂ��� </summary>
    public bool IsLevelUp()
    {
        return player.exp >= player.maxExp;
    }

    /// <summary> �v���C���[�̃��x�����グ�� </summary>
    private void LevelPlayerUp()
    {
        // �z�����o���l���v�Z����
        var exceededExp =  player.exp - player.maxExp;

        // �v���C���[�̃��x�����グ��
        player.lvl++;
        player.exp = exceededExp;
        player.maxExp = CalculateMaxExp();
    }

    /// <summary> �v���C���[�̎��̍ő�o���l���v�Z���� </summary>
    private int CalculateMaxExp()
    {
        return (int)(player.maxExp + Mathf.Pow((float)player.lvl, 1.5f));
    }
}
