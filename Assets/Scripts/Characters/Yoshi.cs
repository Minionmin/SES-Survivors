using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Yoshi : Player
{
    /// <summary> �������Ă��镐�� </summary>
    public List<Arrow> arrows;

    /// <summary> ��̃o�t1���擾�������ǂ����̃t���O </summary>
    public bool IsArrowI = false;

    protected override void Awake()
    {
        // ������
        base.Awake();
    }

    protected override void Start()
    {
        // ���I�u�W�F�N�g�Ƃ̏�����
        base.Start();
    }

    protected override void Update()
    {
        // �U���t���O�̏���
        base.Update();

        // �U���ł��Ȃ��Ȃ牽�����Ȃ�
        if (!canAttack) return;

        // �e�̕����E�ʒu�����Z�b�g
        Attack();
    }

    /// <summary> �U�����n�߂� </summary>
    private void Attack()
    {
        foreach (var arrow in arrows)
        {
            arrow.Launch();
        }

        // �U���\�t���O�����낷
        canAttack = false;
    }

    protected override void LateUpdate()
    {   
        // �������Z�̈ړ�
        base.LateUpdate();
    }


}
