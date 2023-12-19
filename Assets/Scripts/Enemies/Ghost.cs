using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    protected override void Awake()
    {
        // ������
        base.Awake();
    }

    protected override void LateUpdate()
    {
        // �v���C���[��ǂ�������
        base.LateUpdate();
    }

    /// <summary> ���S�O�̏��� </summary>
    public override void Die()
    {
        // �A�j���[�V���� + �t���O
        base.Die();

        // ���S�A�j���[�V�������Đ�
        animator.SetTrigger(DIE);

        // �}�e���A���̃t�F�[�h�v���p�e�B���擾
        var startDissolve = material.GetFloat("_Dissolve");

        // ���S�G�t�F�N�g���Đ����āA���f�����B��
        DOTween.To(() => startDissolve, Dissolve, 0.0f, 0.5f)
            .onComplete = () => { Hide(); };
    }

    /// <summary> �|���ꂽ���Ƀt�F�[�h�A�E�g���� </summary>
    private void Dissolve(float val)
    {
        material.SetFloat("_Dissolve", val);
    }
}
