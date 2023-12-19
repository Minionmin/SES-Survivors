using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary> �G�̃X�e�[�^�X�f�[�^���i�[���Ă���Scriptable Object </summary>
    [SerializeField] protected CharacterData enemyData;

    private Vector3 moveDir;

    #region Stats
    /// <summary> �G�̗̑� </summary>
    protected float hp;
    /// <summary> �G�̍U�� </summary>
    protected float power;
    /// <summary> �G�̈ړ��X�s�[�h </summary>
    protected float movespeed;
    /// <summary> �G�̉�]�X�s�[�h </summary>
    protected float rotateSpeed;
    /// <summary> �v���C���[�Ƃ̍ő�̋��� </summary>
    protected float maxDistanceOffset;
    #endregion

    protected virtual void Awake()
    {
        // �G�̃X�e�[�^�X��������
        InitializeStat();
    }

    protected virtual void LateUpdate()
    {
        // �v���C���[�Əd�Ȃ�Ȃ��悤��
        if (hasReached()) return;
        
        Chase();
    }

    /// <summary> �G�̃X�e�[�^�X�������� </summary>
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

    /// <summary> �C���v�b�g�̕����ɉ�] </summary>
    /// <param name="targetDir"> �ړI�̕��� </param>
    protected void Rotate(Vector3 targetDir)
    {
        Quaternion targetAngle = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotateSpeed);
    }

    /// <summary> �v���C���[��ǂ������� </summary>
    protected void Chase()
    {
        // �ړ����������߂�
        moveDir = (GameManager.Instance.player.transform.position - transform.position).normalized;

        // �ړ�����
        transform.position += moveDir * movespeed * Time.deltaTime;

        // �ړ������ɉ����ĉ�]����
        Rotate(moveDir);
    }

    /// <summary> �v���C���[�ɋ߂Â������ǂ������`�F�b�N���� </summary>
    protected bool hasReached()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= maxDistanceOffset;
    }
}
