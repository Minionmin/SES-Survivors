using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovement, IDamageable
{
    /// <summary> �G�̃X�e�[�^�X�f�[�^���i�[���Ă���Scriptable Object </summary>
    [SerializeField] protected EnemyData enemyData;

    /// <summary> �ړ����� </summary>
    protected Vector3 moveDir;

    #region Component
    /// <summary> �G�̓����蔻�� </summary>
    [SerializeField] protected SphereCollider sphereCollider;

    /// <summary> �G�̃A�j���[�V�����R���g���[�� </summary>
    public Animator animator;

    /// <summary> ���S�A�j���[�V�����g���K�[ </summary>
    protected const string DIE = "Die";

    /// <summary> �G�̃��b�V�������_���[ </summary>
    [SerializeField] protected Renderer bodyRenderer;

    /// <summary> �G���b�V���̃}�e���A�� </summary>
    protected Material material;
    #endregion

    #region Flag
    /// <summary> �G�������邩�ǂ��� </summary>
    protected bool canMove => !isDead;

    /// <summary> �G���|���ꂽ���ǂ��� </summary>
    protected bool isDead = false;
    #endregion

    #region Stats
    /// <summary> �G�̍ő�̗� </summary>
    protected int maxHp;
    /// <summary> �G�̗̑� </summary>
    protected int hp;
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

        // �}�e���A���̕���
        material = bodyRenderer.material;
    }

    protected virtual void LateUpdate()
    {
        // �v���C���[�Əd�Ȃ�Ȃ��悤��
        // �����Ȃ����ɂ͉������Ȃ�
        if (hasReached() || !canMove) return;
        
        Move();
    }

    /// <summary> �G�̃X�e�[�^�X�������� </summary>
    protected virtual void InitializeStat()
    {
        if (enemyData != null)
        {
            maxHp = enemyData.hp;
            hp = enemyData.hp;
            power = enemyData.power;
            movespeed = enemyData.movespeed;
            rotateSpeed = enemyData.rotateSpeed;
            maxDistanceOffset = enemyData.maxDistanceOffset;
        }
    }

    /// <summary> �C���v�b�g�̕����ɉ�] </summary>
    /// <param name="targetDir"> �ړI�̕��� </param>
    public virtual void Rotate(Vector3 targetDir)
    {
        Quaternion targetAngle = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotateSpeed);
    }

    /// <summary> �v���C���[��ǂ������� </summary>
    public virtual void Move()
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

    /// <summary> �_���[�W�����炤���� </summary>
    public virtual void TakeDamage(int damage)
    {
        // �_���[�W���v�Z����
        hp -= damage;

        // �̗͂��}�C�i�X�ɂȂ�Ȃ��悤��
        hp = Mathf.Clamp(hp, 0, maxHp);

        // �̗͂�0�ɂȂ����玀�S��Ԃ̏���������
        if(hp == 0)
        {
            // ���S����
            Die();
        }
    }

    /// <summary> ���S���� </summary>
    public virtual void Die()
    {
        // ���S�t���O�𗧂Ă�
        isDead = true;
    }

    protected void Hide()
    {
        // �e�𗎂Ƃ��Ȃ��悤�ɂ���
        bodyRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        // ���f�����\���ɂ���
        bodyRenderer.enabled = false;

        // �����蔻��𖳌����ɂ���
        sphereCollider.enabled = false;
    }

    public void Spawn()
    {
        // ���S�t���O�����낷
        isDead = false;

        // ���f����\���ɂ���
        bodyRenderer.enabled = true;

        // �����蔻���L�����ɂ���
        sphereCollider.enabled = true;

        // �e�𗎂Ƃ�
        bodyRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
