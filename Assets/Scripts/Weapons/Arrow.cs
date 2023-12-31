using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : Weapon
{
    /// <summary> ����̈З� </summary>
    [SerializeField] protected int power = 15;

    /// <summary> ����̈ړ��X�s�[�h</summary>
    [SerializeField] protected float moveSpeed;

    /// <summary> ����̈ړ����� </summary>
    private Vector3 moveDir = Vector3.zero;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(power);
            Hide();
        }
    }

    protected override void LateUpdate()
    {
        // �����ɓ���
        Travel();
    }

    /// <summary> �e�𔭎˂��� </summary>
    public void Launch()
    {
        var player = GameManager.Instance.player;

        if(player.TryGetComponent<Yoshi>(out Yoshi yoshi))
        {
            // �e���\�����Ă���ʒu�E���������Z�b�g����
            Hide();
            transform.position = player.bulletSpawnPoint.position;

            // 1�ڂ̖�̕�����ݒ�
            if (yoshi.arrows.Count > 0)
            {
                yoshi.arrows[0].SetMoveDir(yoshi.transform.forward);
                yoshi.arrows[0].SetRotation(Quaternion.identity);
            }

            // ��̃o�t1���擾�����ꍇ
            if (yoshi.IsArrowI)
            {
                // 2�ڂ̖�̕�����ݒ�
                Quaternion dirQuaternion = Quaternion.AngleAxis(-45f, Vector3.up);
                Vector3 targetDir = dirQuaternion * player.transform.forward;
                yoshi.arrows[1].SetMoveDir(targetDir);

                // 2�ڂ̖��C�ӂ̕����ɉ�]
                Quaternion rotQuaternion = Quaternion.AngleAxis(-45f, Vector3.up);
                yoshi.arrows[1].SetRotation(rotQuaternion);
            }

            // ���Z�b�g������\���ɂ���
            Show();
        }
    }

    /// <summary> �e���̋��� </summary>
    public virtual void Travel()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    /// <summary> �e�̈ړ��������Z�b�g���� </summary>
    public void SetMoveDir(Vector3 targetDir)
    {
        moveDir = targetDir;
    }

    /// <summary> �e�̉�]���Z�b�g���� </summary>
    public void SetRotation(Quaternion targetRotation)
    {
        transform.localRotation = GameManager.Instance.player.transform.localRotation * targetRotation;
    }

    /// <summary> �e���\������ </summary>
    protected void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary> �e��\������ </summary>
    protected void Show()
    {
        gameObject.SetActive(true);
    }
}
