using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary> �G�̃X�e�[�^�X�f�[�^���i�[���Ă���Scriptable Object </summary>
    [SerializeField] protected CharacterData enemyData;

    #region Stats
    /// <summary> �G�̗̑� </summary>
    protected float hp;
    /// <summary> �G�̍U�� </summary>
    protected float power;
    /// <summary> �G�̈ړ��X�s�[�h </summary>
    protected float movespeed;
    /// <summary> �G�̉�]�X�s�[�h </summary>
    protected float rotateSpeed;
    #endregion

    protected virtual void Awake()
    {
        // �G�̃X�e�[�^�X��������
        InitializeStat();
    }



    /// <summary> �v���C���[�̃X�e�[�^�X�������� </summary>
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
