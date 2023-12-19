using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary> �v���C���[�̃C���v�b�g����������N���X </summary>
    [SerializeField] private PlayerControl playerControl;

    /// <summary> �v���C���[�̃X�e�[�^�X�f�[�^���i�[���Ă���Scriptable Object </summary>
    [SerializeField] protected PlayerData playerData;

    /// <summary> �v���C���[�̃A�j���[�V�����R���g���[�� </summary>
    [SerializeField] protected Animator animator;

    #region Flag
    /// <summary> �v���C���[���ړ����Ă��邩�ǂ��� </summary>
    protected bool isMoving = false;
    #endregion

    #region AnimatorParameter
    protected const string IS_MOVING = "IsMoving"; // �ړ����Ă��邩�ǂ���
    protected const string Attack = "Attack"; // �U���g���K�[
    protected const string Die = "Die"; // ���S�g���K�[
    #endregion

    #region Stats
    /// <summary> �v���C���[�̗̑� </summary>
    protected float hp;
    /// <summary> �v���C���[�̃X�^�~�i </summary>
    protected float stamina;
    /// <summary> �v���C���[�̍U���p���[ </summary>
    protected float power;
    /// <summary> �v���C���[�̈ړ��X�s�[�h </summary>
    protected float movespeed;
    /// <summary> �v���C���[�̉�]�X�s�[�h </summary>
    protected float rotateSpeed = 4;
    #endregion

    protected virtual void Awake()
    {
        // �v���C���[�̃X�e�[�^�X��������
        InitializeStat();
    }

    protected virtual void Start()
    {
        // �J�����̒����Ώۂ��Z�b�g�i���̃I�u�W�F�N�g�j
        CameraManager.Instance.SetCameraTarget(transform);
    }

    protected virtual void Update()
    {
    }

    protected virtual void LateUpdate()
    {
        // �ړ�����
        Move();
    }

    /// <summary> �ړ� </summary>
    protected void Move()
    {
        // �������Q�b�g
        var dir = playerControl.GetMoveDirection();

        // �ړ����Ă��邩�ǂ������m�F
        if (dir != Vector3.zero)
        {
            // �ړ����Ă���t���O�𗧂Ă�
            isMoving = true;

            // �v���C���[��C�ӂ̕����Ɉړ�����
            transform.position += dir * movespeed * Time.deltaTime;

            Rotate(dir);

            // Idle��Ԃ���ړ�������A�j���[�V�������Z�b�g����
            if (isMoving && !animator.GetBool(IS_MOVING))
            {
                animator.SetBool(IS_MOVING, true);
            }
        }
        else
        {
            // �ړ����Ă��Ȃ���΂���Ɋւ���t���O���Z�b�g����
            isMoving = false;
            animator.SetBool(IS_MOVING, false);
        }
    }

    protected void Rotate(Vector3 targetDir)
    {
        Quaternion targetAngle = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotateSpeed);
    }

    /// <summary> �v���C���[�̃X�e�[�^�X�������� </summary>
    protected virtual void InitializeStat()
    {
        if(playerData != null)
        {
            hp = playerData.hp;
            stamina = playerData.stamina;
            power = playerData.power;
            movespeed = playerData.movespeed;
        }
    }
}
