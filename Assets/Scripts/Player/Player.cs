using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMovement
{
    /// <summary> �v���C���[�̃C���v�b�g����������N���X </summary>
    [SerializeField] private PlayerControl playerControl;

    /// <summary> �v���C���[�̃X�e�[�^�X�f�[�^���i�[���Ă���Scriptable Object </summary>
    [SerializeField] protected PlayerData playerData;

    /// <summary> �v���C���[�̃A�j���[�V�����R���g���[�� </summary>
    public Animator animator;

    /// <summary> �U���̃X�^�[�g�|�C���g </summary>
    public Transform bulletSpawnPoint;

    #region Flag
    /// <summary> �v���C���[���ړ����Ă��邩�ǂ��� </summary>
    protected bool isMoving = false;
    /// <summary> �v���C���[���U���ł��邩�ǂ��� </summary>
    protected bool canAttack = false;
    #endregion

    #region AnimatorParameter
    protected const string IS_MOVING = "IsMoving"; // �ړ����Ă��邩�ǂ���
    protected const string DIE = "Die"; // ���S�g���K�[
    #endregion

    #region Stats
    /// <summary> �v���C���[�̃��x�� </summary>
    public int lvl;//[HideInInspector] 
    /// <summary> �v���C���[�̌��݂�Exp </summary>
    public int exp;//[HideInInspector] 
    /// <summary> �v���C���[�̍ő�Exp </summary>
    public int maxExp;//[HideInInspector] 
    /// <summary> �v���C���[�̗̑� </summary>
    protected int hp;
    /// <summary> �v���C���[�̃X�^�~�i </summary>
    protected float stamina;
    /// <summary> �v���C���[�̈ړ��X�s�[�h </summary>
    protected float movespeed;
    /// <summary> �v���C���[�̉�]�X�s�[�h </summary>
    protected float rotateSpeed;
    /// <summary> �v���C���[�̍ő�U���Ԋu </summary>
    protected float attackCooldownMax;
    /// <summary> �v���C���[�̍U���Ԋu </summary>
    protected float attackCooldown;
    #endregion

    protected virtual void Awake()
    {
        // �v���C���[�̃X�e�[�^�X��������
        InitializeStat();

        // �U���Ԋu�̏�����
        attackCooldown = attackCooldownMax;
    }

    protected virtual void Start()
    {
        // �J�����̒����Ώۂ��Z�b�g�i���̃I�u�W�F�N�g�j
        CameraManager.Instance.SetCameraTarget(transform);
    }

    protected virtual void Update()
    {
        UpdateAttack();
    }

    /// <summary> ���̊Ԋu�ōU������ </summary>
    private void UpdateAttack()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0)
        {
            // �U���ł���t���O�𗧂Ă�
            canAttack = true;

            // �U���^�C�}�[�����Z�b�g
            attackCooldown = attackCooldownMax;
        }
    }

    protected virtual void LateUpdate()
    {
        // �ړ�����
        Move();
    }

    /// <summary> �v���C���[�̃X�e�[�^�X�������� </summary>
    protected virtual void InitializeStat()
    {
        if(playerData != null)
        {
            lvl = 1;
            exp = 0;
            maxExp = playerData.maxEXP;
            hp = playerData.hp;
            stamina = playerData.stamina;
            movespeed = playerData.movespeed;
            rotateSpeed = playerData.rotateSpeed;
            attackCooldownMax = playerData.attackCooldownMax;
        }
    }

    /// <summary> �C���v�b�g���ꂽ�����ɉ�] </summary>
    /// <param name="targetDir"> �ړI�̕��� </param>
    public void Rotate(Vector3 targetDir)
    {
        Quaternion targetAngle = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotateSpeed);
    }

    /// <summary> �ړ� </summary>
    public void Move()
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

            // �ړ����Ă�������ɉ�]
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
}
